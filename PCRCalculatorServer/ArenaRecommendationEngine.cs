using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace PCRCalculatorServer;

/// <summary>CPU inference only. Does not mutate battle state or claim simulated wins.</summary>
public sealed class ArenaRecommendationEngine : IDisposable
{
    private readonly InferenceSession encoder;
    private readonly InferenceSession decoder;
    private readonly InferenceSession critic;
    private readonly Manifest manifest;
    private readonly Dictionary<int, int> indices;
    private readonly SemaphoreSlim gate = new(1);

    private sealed class Manifest
    {
        public string format { get; set; }
        public int[] unit_ids { get; set; }
        public int d_model { get; set; }
        public int team_size { get; set; }
        public string[] outcome_names { get; set; }
        public double[] attack_utilities { get; set; }
        public string model_version { get; set; }
        public Dictionary<string, string> onnx_sha256 { get; set; }
    }

    public ArenaRecommendationEngine() : this(Path.Combine(AppContext.BaseDirectory, "Models", "Arena")) { }

    public ArenaRecommendationEngine(string directory)
    {
        manifest = JsonSerializer.Deserialize<Manifest>(File.ReadAllText(Path.Combine(directory, "manifest.json")))
            ?? throw new InvalidDataException("竞技场模型清单为空。");
        if (manifest.format != "pcr_arena_onnx_v1" || manifest.team_size != 5 || manifest.d_model <= 0
            || manifest.unit_ids == null || manifest.unit_ids.Length < 5
            || manifest.unit_ids.Any(x => x <= 0)
            || !manifest.unit_ids.SequenceEqual(manifest.unit_ids.Distinct().OrderBy(x => x))
            || manifest.attack_utilities?.Length != 6 || manifest.outcome_names?.Length != 6)
            throw new InvalidDataException("竞技场模型清单格式不兼容，请重新导出模型。");
        indices = manifest.unit_ids.Select((id, index) => (id, index: index + 2)).ToDictionary(x => x.id, x => x.index);
        foreach (var name in new[] { "encoder.onnx", "decoder.onnx", "critic.onnx" })
        {
            if (manifest.onnx_sha256 == null || !manifest.onnx_sha256.TryGetValue(name, out var expected)
                || !string.Equals(Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(Path.Combine(directory, name)))), expected, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("竞技场模型文件不匹配：" + name);
        }
        using var options = new SessionOptions { IntraOpNumThreads = 1, InterOpNumThreads = 1 };
        encoder = new InferenceSession(Path.Combine(directory, "encoder.onnx"), options);
        try
        {
            decoder = new InferenceSession(Path.Combine(directory, "decoder.onnx"), options);
            critic = new InferenceSession(Path.Combine(directory, "critic.onnx"), options);
        }
        catch
        {
            decoder?.Dispose();
            encoder.Dispose();
            throw;
        }
    }

    public async Task<ArenaSuggestionResponse> SuggestAsync(ArenaSuggestionRequest request, CancellationToken token)
    {
        if (request?.Defense == null || request.Defense.Length != 5 || request.Defense.Distinct().Count() != 5)
            throw new ArgumentException("防守阵容需要五个不同的角色。");
        var unknown = request.Defense.Where(id => !indices.ContainsKey(id)).ToArray();
        if (unknown.Length > 0)
            throw new ArgumentException("模型尚未包含防守角色：" + string.Join(", ", unknown) + "，请更新模型。");
        if (request.Roster == null || request.Roster.Length > 4096)
            throw new ArgumentException("请提供可用角色列表（最多 4096 个）。");
        var available = request.Roster.Distinct().Where(indices.ContainsKey).Select(id => indices[id]).OrderBy(x => x).ToArray();
        if (available.Length < 5)
            throw new ArgumentException("可用角色中至少需要五个模型已知角色。");
        if (request.Candidates < 1 || request.Candidates > 512 || request.Count < 1 || request.Count > 20)
            throw new ArgumentException("候选数量应为 1–512，返回数量应为 1–20。");
        await gate.WaitAsync(token);
        try { return await Task.Run(() => Suggest(request, available, token), token); }
        finally { gate.Release(); }
    }

    private ArenaSuggestionResponse Suggest(ArenaSuggestionRequest request, int[] available, CancellationToken token)
    {
        var watch = Stopwatch.StartNew();
        var defense = request.Defense.OrderBy(x => x).Select(id => (long)indices[id]).ToArray();
        token.ThrowIfCancellationRequested();
        var encoded = Run(encoder, Input("opponents", defense, 1, 5), Input("goals", new long[] { 0 }, 1));
        var random = new Random(request.Seed);
        var teams = new Dictionary<string, long[]>();
        // Upstream uses 64-wide nucleus sampling, top_p=.95 and temperature=1.
        for (var attempt = 0; attempt < 40 && teams.Count < request.Candidates; attempt++)
        {
            token.ThrowIfCancellationRequested();
            var batch = Math.Min(64, request.Candidates - teams.Count + 16);
            var tokens = new long[batch * 5];
            var memory = new float[batch * encoded.Length];
            for (var row = 0; row < batch; row++)
            {
                tokens[row * 5] = 1; // BOS; future PAD tokens are behind the causal mask.
                Array.Copy(encoded, 0, memory, row * encoded.Length, encoded.Length);
            }
            var chosen = new long[batch][];
            for (var row = 0; row < batch; row++) chosen[row] = new long[5];
            for (var position = 0; position < 5; position++)
            {
                token.ThrowIfCancellationRequested();
                var logits = Run(decoder, Input("tokens", tokens, batch, 5),
                    NamedOnnxValue.CreateFromTensor("memory", new DenseTensor<float>(memory, new[] { batch, 6, manifest.d_model })));
                for (var row = 0; row < batch; row++)
                {
                    var previous = position == 0 ? 1 : (int)chosen[row][position - 1];
                    var legal = available.Take(available.Length - (4 - position)).Where(index => index > previous).ToArray();
                    var offset = (row * 5 + position) * (manifest.unit_ids.Length + 2);
                    var selected = Sample(logits, offset, legal, random);
                    chosen[row][position] = selected;
                    if (position < 4) tokens[row * 5 + position + 1] = selected;
                }
            }
            foreach (var team in chosen)
            {
                teams.TryAdd(string.Join(",", team), team);
                if (teams.Count == request.Candidates) break;
            }
            // Exactly five available units have only one possible team.
            if (available.Length == 5) break;
        }
        var rows = teams.Values.ToArray();
        var ranked = new List<ArenaTeamSuggestion>();
        for (var start = 0; start < rows.Length; start += 64)
        {
            token.ThrowIfCancellationRequested();
            var batch = Math.Min(64, rows.Length - start);
            var attacks = rows.Skip(start).Take(batch).SelectMany(x => x).ToArray();
            var defenses = Enumerable.Range(0, batch).SelectMany(_ => defense).ToArray();
            var logits = Run(critic, Input("attacks", attacks, batch, 5), Input("defends", defenses, batch, 5));
            for (var row = 0; row < batch; row++)
            {
                var p = Softmax(logits.Skip(row * 6).Take(6).Select(x => (double)x).ToArray());
                ranked.Add(new ArenaTeamSuggestion
                {
                    Team = rows[start + row].Select(index => manifest.unit_ids[(int)index - 2]).ToArray(),
                    PredictedWinRate = p.Take(4).Sum(),
                    Score = p.Select((value, i) => value * manifest.attack_utilities[i]).Sum(),
                    Probabilities = p
                });
            }
        }
        return new ArenaSuggestionResponse
        {
            Teams = ranked.OrderByDescending(x => x.Score).Take(request.Count).ToArray(),
            GeneratedCandidates = rows.Length, ElapsedMilliseconds = watch.ElapsedMilliseconds,
            ModelVersion = manifest.model_version, UnknownRoster = request.Roster.Distinct().Where(id => !indices.ContainsKey(id)).OrderBy(x => x).ToArray()
        };
    }

    private static int Sample(float[] logits, int offset, int[] legal, Random random)
    {
        if (legal.Length == 0) throw new InvalidDataException("模型无法补全五人阵容。");
        var ordered = legal.OrderByDescending(index => logits[offset + index]).ToArray();
        var probabilities = Softmax(ordered.Select(index => (double)logits[offset + index]).ToArray());
        var cumulative = 0.0;
        var keep = 0;
        do { cumulative += probabilities[keep++]; } while (keep < ordered.Length && cumulative < .95);
        var sample = random.NextDouble() * cumulative;
        for (var i = 0; i < keep; i++)
        {
            sample -= probabilities[i];
            if (sample <= 0) return ordered[i];
        }
        return ordered[keep - 1];
    }

    private static double[] Softmax(double[] logits)
    {
        var maximum = logits.Max();
        var values = logits.Select(value => Math.Exp(value - maximum)).ToArray();
        var total = values.Sum();
        if (!double.IsFinite(total) || total <= 0) throw new InvalidDataException("模型返回了无效数值。");
        return values.Select(value => value / total).ToArray();
    }

    private static NamedOnnxValue Input(string name, long[] values, params int[] shape) =>
        NamedOnnxValue.CreateFromTensor(name, new DenseTensor<long>(values, shape));

    private static float[] Run(InferenceSession session, params NamedOnnxValue[] inputs)
    {
        using var output = session.Run(inputs);
        return output.First().AsTensor<float>().ToArray();
    }

    public void Dispose()
    {
        encoder.Dispose(); decoder.Dispose(); critic.Dispose(); gate.Dispose();
    }
}

public sealed class ArenaSuggestionRequest
{
    public int[] Defense { get; set; }
    public int[] Roster { get; set; }
    public int Candidates { get; set; } = 256;
    public int Count { get; set; } = 8;
    public int Seed { get; set; } = 42;
}

public sealed class ArenaTeamSuggestion
{
    public int[] Team { get; set; }
    public double PredictedWinRate { get; set; }
    public double Score { get; set; }
    public double[] Probabilities { get; set; }
}

public sealed class ArenaSuggestionResponse
{
    public ArenaTeamSuggestion[] Teams { get; set; }
    public int GeneratedCandidates { get; set; }
    public long ElapsedMilliseconds { get; set; }
    public string ModelVersion { get; set; }
    public int[] UnknownRoster { get; set; }
}
