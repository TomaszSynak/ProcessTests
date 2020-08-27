namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit.Abstractions;

    internal class Logger
    {
        private readonly TimeSpan _tokenExpiration;

        private readonly ITestOutputHelper _outputHelper;

        public Logger(ITestOutputHelper outputHelper, TimeSpan tokenExpiration)
        {
            _outputHelper = outputHelper;
            _tokenExpiration = tokenExpiration;
        }

        public void Log(string msg) => _outputHelper.WriteLine(msg);

        public async Task Run(Func<CancellationToken, Task> functionToRun, string functionName)
        {
            var cts = new CancellationTokenSource(_tokenExpiration);
            try
            {
                await functionToRun.Invoke(cts.Token);
            }
            catch (TaskCanceledException)
            {
                _outputHelper.WriteLine($"{functionName} - request took longer than {_tokenExpiration}");
                throw;
            }
            catch (Exception exception)
            {
                _outputHelper.WriteLine($"{functionName} - {exception.Message}");
                throw;
            }
            finally
            {
                cts.Dispose();
            }
        }

        public async Task Run<T1>(Func<T1, CancellationToken, Task> functionToRun, T1 arg1, string functionName)
        {
            var cts = new CancellationTokenSource(_tokenExpiration);
            try
            {
                await functionToRun.Invoke(arg1, cts.Token);
            }
            catch (TaskCanceledException)
            {
                _outputHelper.WriteLine($"{functionName} - request took longer than {_tokenExpiration}");
                throw;
            }
            catch (Exception exception)
            {
                _outputHelper.WriteLine($"{functionName} - {exception.Message}");
                throw;
            }
            finally
            {
                cts.Dispose();
            }
        }

        public async Task<TResult> Run<TResult>(Func<CancellationToken, Task<TResult>> functionToRun, string functionName)
        {
            var cts = new CancellationTokenSource(_tokenExpiration);
            try
            {
                return await functionToRun.Invoke(cts.Token);
            }
            catch (TaskCanceledException)
            {
                _outputHelper.WriteLine($"{functionName} - request took longer than {_tokenExpiration}");
                throw;
            }
            catch (Exception exception)
            {
                _outputHelper.WriteLine($"{functionName} - {exception.Message}");
                throw;
            }
            finally
            {
                cts.Dispose();
            }
        }

        public async Task<TResult> Run<T1, TResult>(Func<T1, CancellationToken, Task<TResult>> functionToRun, T1 arg1, string functionName)
        {
            var cts = new CancellationTokenSource(_tokenExpiration);
            try
            {
                return await functionToRun.Invoke(arg1, cts.Token);
            }
            catch (TaskCanceledException)
            {
                _outputHelper.WriteLine($"{functionName} - request took longer than {_tokenExpiration}");
                throw;
            }
            catch (Exception exception)
            {
                _outputHelper.WriteLine($"{functionName} - {exception.Message}");
                throw;
            }
            finally
            {
                cts.Dispose();
            }
        }
    }
}
