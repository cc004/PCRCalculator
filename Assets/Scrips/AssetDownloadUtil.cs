using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 资源下载统一入口：限流并发、失败重试、字节级进度。
/// AB manifest（JP/CN）、数据库、战斗资源都走这里，避免各处自己写一套阻塞式下载。
/// </summary>
public static class AssetDownloadUtil
{
    /// <summary>默认并发数：同时进行的 HTTP 请求上限。</summary>
    public const int DefaultConcurrency = 6;

    /// <summary>单个请求的最大尝试次数（含首次）。</summary>
    public const int MaxAttempts = 3;

    /// <summary>默认 User-Agent：GitHub API 对缺失该头的请求直接返回 403。</summary>
    public const string UserAgent = "PCRCalculator/1.0 (+https://github.com/acygen/PCRCalculator)";

    private static readonly TimeSpan AttemptTimeout = TimeSpan.FromSeconds(120);

    /// <summary>manifest 抓取共用的并发闸门，避免递归展开时并发数无限放大。</summary>
    public static SemaphoreSlim ManifestGate { get; } = new SemaphoreSlim(DefaultConcurrency, DefaultConcurrency);

    private static readonly HttpClient client = CreateClient();

    private static long bytesReceived;
    private static long completedRequests;
    private static long failedRequests;

    /// <summary>进程内累计已接收字节数（所有下载合计）。</summary>
    public static long TotalBytesReceived => Interlocked.Read(ref bytesReceived);

    /// <summary>进程内累计成功完成的请求数。</summary>
    public static long CompletedRequests => Interlocked.Read(ref completedRequests);

    /// <summary>进程内累计失败的请求数（重试耗尽）。</summary>
    public static long FailedRequests => Interlocked.Read(ref failedRequests);

    /// <summary>
    /// 单次下载的字节进度快照。下载线程原地更新，UI 线程只读，字段用 Interlocked 保护。
    /// </summary>
    public sealed class ByteProgress
    {
        private long received;
        private long total = -1;

        public long Received => Interlocked.Read(ref received);

        /// <summary>总字节数；服务端没给 Content-Length 时为 -1。</summary>
        public long Total => Interlocked.Read(ref total);

        public bool HasTotal => Total > 0;

        /// <summary>0~1 的完成度；总长未知时返回 0。</summary>
        public double Fraction
        {
            get
            {
                var totalBytes = Total;
                if (totalBytes <= 0)
                    return 0d;
                return Math.Min(1d, (double)Received / totalBytes);
            }
        }

        public void Set(long receivedBytes, long totalBytes)
        {
            Interlocked.Exchange(ref received, receivedBytes);
            if (totalBytes > 0)
                Interlocked.Exchange(ref total, totalBytes);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref received, 0);
            Interlocked.Exchange(ref total, -1);
        }
    }

    private static HttpClient CreateClient()
    {
        try
        {
            // Mono 的连接池默认只有 2，并发下载会被卡住；.NET Core 下该设置无效但不报错。
            ServicePointManager.DefaultConnectionLimit = 32;
        }
        catch (Exception)
        {
            // 忽略：某些平台的 ServicePointManager 不可配置
        }

        // 超时一律交给每次尝试的 CancellationTokenSource，避免大文件被整体超时切断。
        var result = new HttpClient { Timeout = Timeout.InfiniteTimeSpan };
        try
        {
            result.DefaultRequestHeaders.ConnectionClose = false;
        }
        catch (Exception)
        {
            // 忽略：个别运行时不支持该头
        }

        try
        {
            // GitHub API 对没有 User-Agent 的请求直接返回 403，必须显式带上。
            result.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent);
        }
        catch (Exception)
        {
            // 忽略：个别运行时不支持该头
        }

        return result;
    }

    /// <summary>同步下载（阻塞当前线程）。新代码优先用 <see cref="DownloadAsync"/>。</summary>
    public static byte[] Download(string url) => Download(url, null);

    /// <summary>同步下载（阻塞当前线程），可传入字节进度。</summary>
    public static byte[] Download(string url, ByteProgress progress)
        => DownloadAsync(url, progress, CancellationToken.None).GetAwaiter().GetResult();

    public static Task<byte[]> DownloadAsync(string url) => DownloadAsync(url, null, CancellationToken.None);

    /// <summary>带重试的异步下载；progress 会被原地更新。</summary>
    public static async Task<byte[]> DownloadAsync(string url, ByteProgress progress, CancellationToken cancellationToken = default)
    {
        Exception lastException = null;
        for (var attempt = 1; attempt <= MaxAttempts; attempt++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                var result = await DownloadOnceAsync(url, progress, cancellationToken).ConfigureAwait(false);
                Interlocked.Increment(ref completedRequests);
                return result;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                lastException = ex;
                if (attempt >= MaxAttempts || !IsTransient(ex))
                    break;

                var delayMs = GetRetryDelayMs(attempt);
                LogWarning(
                    $"download attempt {attempt}/{MaxAttempts} failed for {url}, retrying in {delayMs}ms: " +
                    $"{ex.GetType().Name}: {ex.Message}");
                await Task.Delay(delayMs, cancellationToken).ConfigureAwait(false);
            }
        }

        Interlocked.Increment(ref failedRequests);
        throw lastException ?? new InvalidOperationException($"download failed for {url}");
    }

    private static async Task<byte[]> DownloadOnceAsync(string url, ByteProgress progress, CancellationToken cancellationToken)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(AttemptTimeout);

        using var response = await client
            .GetAsync(url, HttpCompletionOption.ResponseHeadersRead, timeout.Token)
            .ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            var message = $"download http {(int)response.StatusCode} {response.ReasonPhrase} for {url}";
            if (IsTransientStatusCode(response.StatusCode))
                throw new HttpRequestException(message);

            throw new InvalidOperationException(message);
        }

        var contentLength = response.Content.Headers.ContentLength ?? -1;
        progress?.Set(0, contentLength);

        using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        var buffer = new byte[81920];
        long fileBytes = 0;
        using var memory = contentLength > 0 && contentLength <= int.MaxValue
            ? new MemoryStream((int)contentLength)
            : new MemoryStream();

        while (true)
        {
            var read = await stream.ReadAsync(buffer, 0, buffer.Length, timeout.Token).ConfigureAwait(false);
            if (read <= 0)
                break;

            memory.Write(buffer, 0, read);
            fileBytes += read;
            Interlocked.Add(ref bytesReceived, read);
            progress?.Set(fileBytes, contentLength);
        }

        return memory.ToArray();
    }

    private static bool IsTransientStatusCode(HttpStatusCode statusCode)
    {
        var code = (int)statusCode;
        return code == 408 || code == 425 || code == 429 || code >= 500;
    }

    private static bool IsTransient(Exception ex)
    {
        if (ex is HttpRequestException || ex is IOException)
            return true;
        // 单次尝试超时（CancelAfter）也按可重试处理
        if (ex is OperationCanceledException || ex is TaskCanceledException)
            return true;
        if (ex is WebException webException)
            return IsTransientWebException(webException);
        if (ex is AggregateException aggregateException)
            return aggregateException.Flatten().InnerExceptions.Any(IsTransient);

        return false;
    }

    private static bool IsTransientWebException(WebException ex)
    {
        return ex.Status == WebExceptionStatus.Timeout ||
               ex.Status == WebExceptionStatus.RequestCanceled ||
               ex.Status == WebExceptionStatus.ConnectFailure ||
               ex.Status == WebExceptionStatus.ConnectionClosed ||
               ex.Status == WebExceptionStatus.ReceiveFailure ||
               ex.Status == WebExceptionStatus.SendFailure ||
               ex.Status == WebExceptionStatus.KeepAliveFailure ||
               ex.Status == WebExceptionStatus.NameResolutionFailure ||
               ex.Status == WebExceptionStatus.ProxyNameResolutionFailure ||
               ex.Status == WebExceptionStatus.PipelineFailure ||
               ex.Status == WebExceptionStatus.UnknownError;
    }

    private static int GetRetryDelayMs(int attempt)
    {
        return attempt switch
        {
            1 => 500,
            2 => 1500,
            _ => 3000
        };
    }

    /// <summary>
    /// 以固定并发处理一组任务，返回与输入同序的结果数组。
    /// body 内部是同步重活也没关系——它跑在线程池线程上，不占主线程。
    /// </summary>
    public static async Task<TResult[]> RunBoundedAsync<TItem, TResult>(
        IEnumerable<TItem> items,
        Func<TItem, Task<TResult>> body,
        int concurrency,
        CancellationToken cancellationToken = default)
    {
        if (items == null || body == null)
            return Array.Empty<TResult>();

        var list = items as IList<TItem> ?? items.ToList();
        if (list.Count == 0)
            return Array.Empty<TResult>();

        var limit = Math.Max(1, concurrency);
        using var gate = new SemaphoreSlim(limit, limit);
        var tasks = new Task<TResult>[list.Count];
        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];
            tasks[i] = Task.Run(async () =>
            {
                await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    return await body(item).ConfigureAwait(false);
                }
                finally
                {
                    gate.Release();
                }
            }, cancellationToken);
        }

        return await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    /// <summary>把并发异步方法包成同步阻塞调用（给还在同步上下文里的调用方用）。</summary>
    public static void RunSync(Func<Task> func)
    {
        if (func == null)
            return;

        if (SynchronizationContext.Current == null)
        {
            func().GetAwaiter().GetResult();
            return;
        }

        // 有同步上下文时直接阻塞会死锁，改在独立线程上跑。
        Task.Run(func).GetAwaiter().GetResult();
    }

    /// <summary>把并发异步方法包成同步阻塞调用并取回结果。</summary>
    public static T RunSync<T>(Func<Task<T>> func)
    {
        if (func == null)
            return default;

        if (SynchronizationContext.Current == null)
            return func().GetAwaiter().GetResult();

        return Task.Run(func).GetAwaiter().GetResult();
    }

    public static void LogWarning(string message)
    {
#if UNITY_5_3_OR_NEWER || UNITY_EDITOR
        UnityEngine.Debug.LogWarning(message);
#else
        Console.WriteLine(message);
#endif
    }

    public static void LogError(string message)
    {
#if UNITY_5_3_OR_NEWER || UNITY_EDITOR
        UnityEngine.Debug.LogError(message);
#else
        Console.WriteLine(message);
#endif
    }
}
