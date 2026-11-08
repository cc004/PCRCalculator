using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCRCaculator
{
    /// <summary>Owns the companion process for the client session, independently of calculation tasks.</summary>
    public static class OfflineServerManager
    {
        private static readonly object Gate = new object();
        private static OfflineServerSession current;
        private static string currentContext;
        private static bool closing;

        static OfflineServerManager()
        {
            AppDomain.CurrentDomain.ProcessExit += (_, __) => Shutdown();
        }

#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitializeClientSession()
        {
            // Also runs when Editor Play Mode has domain reload disabled.
            Shutdown();
            lock (Gate) closing = false;
            UnityEngine.Application.quitting -= Shutdown;
            UnityEngine.Application.quitting += Shutdown;
        }
#endif

        public static int? ProcessId
        {
            get { lock (Gate) return current != null && current.IsUsable ? (int?)current.ProcessId : null; }
        }

        internal static bool IsClosing
        {
            get { lock (Gate) return closing; }
        }

        internal static OfflineServerSession Acquire()
        {
            var context = RemoteSimulator.GetServerContext();
            lock (Gate)
            {
                if (closing)
                    throw new OperationCanceledException("脱机模拟服务已关闭。");
                if (current != null && current.IsUsable && currentContext == context)
                    return current;

                // A child loads its database selection once. Never reuse it for a different version.
                current?.Dispose();
                current = null;
                currentContext = null;
                current = new OfflineServerSession();
                currentContext = context;
                return current;
            }
        }

        public static void Shutdown()
        {
            lock (Gate)
            {
                closing = true;
                current?.Dispose();
                current = null;
                currentContext = null;
            }
        }
    }

    internal sealed class OfflineServerSession : IDisposable
    {
        private static readonly HttpClient Client = new HttpClient(new HttpClientHandler { UseProxy = false });
        private readonly Process process;
        private readonly int port;
        private readonly CancellationTokenSource lifetime = new CancellationTokenSource();
        private readonly CancellationToken token;
        private readonly StringBuilder stderr = new StringBuilder();
        private readonly Task ready;
        private int disposed;

        internal OfflineServerSession()
        {
            token = lifetime.Token;
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            process = Process.Start(RemoteSimulator.CreateServerStartInfo(port))
                ?? throw new InvalidOperationException("无法启动脱机模拟程序。");
            try
            {
                process.ErrorDataReceived += (_, data) =>
                {
                    if (data.Data == null) return;
                    lock (stderr)
                        if (stderr.Length < 2000) stderr.AppendLine(data.Data);
                };
                process.BeginErrorReadLine();
                ready = WaitForServer();
                // Retire failed startup immediately and observe the exception even if the task is cancelled.
                ready.ContinueWith(failed => { var error = failed.Exception; Dispose(); },
                    CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        internal int ProcessId => process.Id;

        internal bool IsUsable
        {
            get
            {
                if (Volatile.Read(ref disposed) != 0) return false;
                try { return !process.HasExited && !ready.IsFaulted && !ready.IsCanceled; }
                catch (InvalidOperationException) { return false; }
            }
        }

        private string ErrorText()
        {
            lock (stderr) return stderr.ToString().Trim();
        }

        private async Task WaitForServer()
        {
            using (var startup = CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                startup.CancelAfter(TimeSpan.FromSeconds(30));
                try
                {
                    while (true)
                    {
                        startup.Token.ThrowIfCancellationRequested();
                        if (process.HasExited)
                        {
                            process.WaitForExit();
                            throw new InvalidOperationException($"脱机模拟程序已退出（{process.ExitCode}）。\n" + ErrorText());
                        }
                        try
                        {
                            // Any response, including 404, means ASP.NET is accepting requests.
                            using (var response = await Client.GetAsync($"http://localhost:{port}/", startup.Token).ConfigureAwait(false))
                                return;
                        }
                        catch (HttpRequestException)
                        {
                            await Task.Delay(100, startup.Token).ConfigureAwait(false);
                        }
                    }
                }
                catch (OperationCanceledException) when (!token.IsCancellationRequested)
                {
                    throw new TimeoutException("脱机模拟程序启动超时。\n" + ErrorText());
                }
            }
        }

        internal async Task<string> Send(string json, CancellationToken taskToken, string endpoint = "/simulate")
        {
            using (var linked = CancellationTokenSource.CreateLinkedTokenSource(taskToken, token))
            {
                linked.Token.ThrowIfCancellationRequested();
                // Task cancellation must not cancel shared startup or wait for it to finish.
                var cancelled = new TaskCompletionSource<bool>();
                using (linked.Token.Register(() => cancelled.TrySetCanceled()))
                {
                    var completed = await Task.WhenAny(ready, cancelled.Task).ConfigureAwait(false);
                    await completed.ConfigureAwait(false);
                }
                linked.Token.ThrowIfCancellationRequested();
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                using (var response = await Client.PostAsync($"http://localhost:{port}{endpoint}", content, linked.Token).ConfigureAwait(false))
                {
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    linked.Token.ThrowIfCancellationRequested();
                    if (!response.IsSuccessStatusCode)
                        throw new HttpRequestException("脱机模拟请求失败（HTTP " + (int)response.StatusCode + "）："
                            + (result.Length > 2000 ? result.Substring(0, 2000) : result));
                    return result;
                }
            }
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref disposed, 1) != 0) return;
            lifetime.Cancel();
            try
            {
                if (!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit(1000);
                }
            }
            catch (InvalidOperationException) { }
            catch (System.ComponentModel.Win32Exception error) { Trace.TraceWarning(error.Message); }
            finally
            {
                process.Dispose();
                lifetime.Dispose();
            }
        }
    }
}
