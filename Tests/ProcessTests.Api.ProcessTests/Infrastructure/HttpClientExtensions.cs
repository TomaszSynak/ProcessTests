namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class HttpClientExtensions
    {
        public static async Task ExecuteRequest(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        public static async Task<TResult> ExecuteRequest<TResult>(this HttpClient httpClient, HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadAsStringAsync()).Deserialize<TResult>();
        }
    }
}
