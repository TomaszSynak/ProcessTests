namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    using System.IO;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Logging;

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("ProcessTests");

            base.ConfigureWebHost(builder);

            builder.UseContentRoot(Directory.GetCurrentDirectory());
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = true)
                .ConfigureLogging(builder => builder.ClearProviders());
        }
    }
}
