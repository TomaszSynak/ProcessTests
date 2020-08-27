namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc.Testing;

    internal class HttpClientFactory
    {
        public static HttpClient Create(CustomWebApplicationFactory<Startup> appFactory)
        {
            var httpClient = appFactory
                .WithWebHostBuilder(config => { })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            httpClient.DefaultRequestHeaders.Accept.Clear();

            return httpClient;
        }
    }
}
