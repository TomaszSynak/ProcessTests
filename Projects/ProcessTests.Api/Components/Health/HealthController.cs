namespace ProcessTests.Api.Components.Health
{
    using System.Net;
    using System.Reflection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HealthController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HealthController(IWebHostEnvironment webHostEnvironment)
            => _webHostEnvironment = webHostEnvironment;

        private static string ApiVersion
            => Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion ?? string.Empty;

        /// <summary>
        /// Endpoint to verify server's responsiveness.
        /// </summary>
        /// <response code="200"> Server is responsive </response>
        /// <response code="424"> Server is unresponsive </response>
        [HttpGet]
        [ProducesResponseType(typeof(HealthStateDto), (int)HttpStatusCode.OK)]
        public IActionResult Health()
        {
            var healthState = new HealthStateDto
            {
                Name = _webHostEnvironment.ApplicationName,
                Environment = _webHostEnvironment.EnvironmentName,
                ApiVersion = ApiVersion,
                IsHealthy = true
            };

            return StatusCode((int)HttpStatusCode.OK, healthState);
        }
    }
}
