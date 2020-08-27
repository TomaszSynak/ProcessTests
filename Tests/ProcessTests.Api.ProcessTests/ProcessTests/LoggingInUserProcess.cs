namespace ProcessTests.Api.ProcessTests.ProcessTests
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Components.Authenticate.Models;
    using Infrastructure;

    internal class LoggingInUserProcess : IProcess<Task<JwtTokenDto>>
    {
        private readonly Logger _logger;

        private readonly HttpClient _httpClient;

        private readonly HttpRequestFactory _httpRequestFactory;

        public LoggingInUserProcess(Logger logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpRequestFactory = new HttpRequestFactory(null);
        }

        public async Task<JwtTokenDto> Run()
        {
            // #1 [User] Log in user
            var token = await _logger.Run(LoggingIn, $"{nameof(LoggingIn)}");

            // #2 [User] Verify JWT token
            await _logger.Run(CheckAuthToken, token, $"{nameof(CheckAuthToken)}");

            return token;
        }

        private async Task<JwtTokenDto> LoggingIn(CancellationToken cancellationToken = default)
        {
            var url = ApiEndpoints.CreateAuthToken();

            var requestBody = new CredentialsDto { Email = "processtestuser@email.com", Password = "processtestpass" }.ToStringContent();

            var requestMessage = _httpRequestFactory.CreatePostRequest(url, requestBody);

            return await _httpClient.ExecuteRequest<JwtTokenDto>(requestMessage, cancellationToken);
        }

        private async Task CheckAuthToken(JwtTokenDto authToken, CancellationToken cancellationToken = default)
        {
            var url = ApiEndpoints.CheckAuthToken();

            var requestMessage = HttpRequestFactory.CreateAuthGetRequest(url, authToken);

            await _httpClient.ExecuteRequest(requestMessage, cancellationToken);
        }
    }
}
