using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;

namespace PCRCalculatorServer;

public sealed class ArenaModelProvider : IDisposable
{
    private readonly Lazy<ArenaRecommendationEngine> engine = new(() => new ArenaRecommendationEngine());
    public ArenaRecommendationEngine Engine => engine.Value;
    public void Dispose() { if (engine.IsValueCreated) engine.Value.Dispose(); }
}

[ApiController]
public sealed class ArenaController : ControllerBase
{
    private readonly ArenaModelProvider models;
    public ArenaController(ArenaModelProvider models) { this.models = models; }

    [HttpPost("/arena/suggest")]
    public async Task<ActionResult<ArenaSuggestionResponse>> Suggest(ArenaSuggestionRequest request, CancellationToken token)
    {
        try { return await models.Engine.SuggestAsync(request, token); }
        catch (ArgumentException error) { return BadRequest(new { message = error.Message }); }
        catch (Exception error) when (error is IOException || error is OnnxRuntimeException || error is DllNotFoundException)
        {
            return StatusCode(503, new { message = "无法加载竞技场建议模型，请更新脱机程序与 Models/Arena 文件夹。" + error.Message });
        }
    }
}
