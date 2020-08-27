namespace ProcessTests.Api.ProcessTests.ProcessTests
{
    using System;
    using Components.Authenticate.Models;

    internal static class ProcessTestSharedData
    {
        public static TimeSpan TokenExpiration { get; private set; }

        public static bool RunLoggingInUserProcessTest { get; private set; }

        public static bool RunUpdatingUserEmailProcessTest { get; private set; }

        public static JwtTokenDto AppToken { get; set; }

        public static void Configure(ProcessTestSettings settings)
        {
            TokenExpiration = settings.TokenExpiration;
            RunLoggingInUserProcessTest = settings.RunLoggingInUserProcessTest;
            RunUpdatingUserEmailProcessTest = settings.RunUpdatingUserEmailProcessTest;
        }
    }
}
