namespace ProcessTests.Api.Components.Authenticate
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [ApiController]
    [Authorize(AuthenticationSchemes = "JwtAuth")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) => _authService = authService;

        /// <summary>
        /// Endpoint to verify JWT authorization token
        /// </summary>
        /// <response code="200"> JWT authorization token is correct </response>
        /// <response code="401"> JWT authorization token is incorrect </response>
        [HttpGet("Check")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public IActionResult AuthCheck()
        {
            return string.IsNullOrEmpty(HttpContext.GetEmail())
                ? StatusCode((int)HttpStatusCode.Unauthorized, "JWT token is incorrect.")
                : StatusCode((int)HttpStatusCode.OK, "JWT token is correct.");
        }

        /// <summary>
        /// Endpoint to obtain JWT authorization token
        /// </summary>
        /// <param name="credentialsDto"> User's credentials </param>
        /// <param name="cancellationToken"> Cancellation Token to pass </param>
        /// <response code="201"> Returns JWT token </response>
        /// <response code="401"> User's credentials are incorrect </response>
        [HttpPost]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(JwtTokenDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Auth(CredentialsDto credentialsDto, CancellationToken cancellationToken)
        {
            var jwtToken = await _authService.GetJwtToken(credentialsDto, cancellationToken);

            return jwtToken == null
                ? StatusCode((int)HttpStatusCode.Unauthorized, "User's credentials are incorrect.")
                : StatusCode((int)HttpStatusCode.Created, jwtToken);
        }

        /// <summary>
        /// Endpoint to update user's email
        /// </summary>
        /// <param name="userEmail"> New user's email </param>
        /// <response code="200"> Email has been updated </response>
        [HttpPost("user/email")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateEmail([FromBody]string userEmail)
        {
            return string.IsNullOrEmpty(userEmail)
                ? StatusCode((int)HttpStatusCode.BadRequest, string.Empty)
                : StatusCode((int)HttpStatusCode.OK, "Email has been updated.");
        }
    }
}
