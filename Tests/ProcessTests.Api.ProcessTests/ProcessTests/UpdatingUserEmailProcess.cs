namespace ProcessTests.Api.ProcessTests.ProcessTests
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Components.Authenticate.Models;
    using Infrastructure;

    internal class UpdatingUserEmailProcess : IProcess<Task>
    {
        private readonly Logger _logger;

        private readonly HttpClient _httpClient;

        private readonly HttpRequestFactory _httpRequestFactory;

        public UpdatingUserEmailProcess(Logger logger, HttpClient httpClient, JwtTokenDto jwtTokenDto)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpRequestFactory = new HttpRequestFactory(jwtTokenDto);
        }

        public async Task Run()
        {
            // #1 [User] Update user's email
            await _logger.Run(UpdatingEmail, $"{nameof(UpdatingEmail)}");
        }

        private async Task UpdatingEmail(CancellationToken cancellationToken = default)
        {
            var url = ApiEndpoints.UpdateUserEmail();

            var newUserEmail = "updatedprocesstest@email.com".ToStringContent();

            var requestMessage = _httpRequestFactory.CreateAuthPostRequest(url, newUserEmail);

            await _httpClient.ExecuteRequest(requestMessage, cancellationToken);
        }
    }
}
