namespace ProcessTests.Api.ProcessTests.ProcessTests
{
    using System.Threading.Tasks;
    using Infrastructure;
    using Xunit;
    using Xunit.Abstractions;

    [Collection("ProcessTests-HappyPath")]
    [TestCaseOrderer("ProcessTests.Api.ProcessTests.Infrastructure.TestPriorityOrderer", "ProcessTests.Api.ProcessTests")]
    public class ProcessTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly Logger _logger;

        private readonly CustomWebApplicationFactory<Startup> _appFactory;

        static ProcessTests()
        {
            ProcessTestSharedData.Configure(SettingsProvider.Get<ProcessTestSettings>());
        }

        public ProcessTests(ITestOutputHelper testOutputHelper, CustomWebApplicationFactory<Startup> appFactory)
        {
            _appFactory = appFactory;

            _logger = new Logger(testOutputHelper, ProcessTestSharedData.TokenExpiration);
        }

        [Fact]
        [TestPriority(1)]
        public async Task LoggingInUser()
        {
            if (!ProcessTestSharedData.RunLoggingInUserProcessTest)
            {
                _logger.Log($"{nameof(LoggingInUser)}ProcessTest has been skipped.");
                return;
            }

            // Arrange
            var loggingInUserProcess = new LoggingInUserProcess(_logger, HttpClientFactory.Create(_appFactory));

            // Act
            var exception = await Record.ExceptionAsync(async () =>
            {
                ProcessTestSharedData.AppToken = await loggingInUserProcess.Run();
            });

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        [TestPriority(2)]
        public async Task UpdatingUserEmail()
        {
            if (!ProcessTestSharedData.RunUpdatingUserEmailProcessTest)
            {
                _logger.Log($"{nameof(UpdatingUserEmail)}ProcessTest has been skipped.");
                return;
            }

            // Arrange
            var updatingUserEmailProcess = new UpdatingUserEmailProcess(_logger, HttpClientFactory.Create(_appFactory), ProcessTestSharedData.AppToken);

            // Act
            var exception = await Record.ExceptionAsync(updatingUserEmailProcess.Run);

            // Assert
            Assert.Null(exception);
        }
    }
}
