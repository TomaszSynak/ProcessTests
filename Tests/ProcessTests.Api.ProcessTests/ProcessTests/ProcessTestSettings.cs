namespace ProcessTests.Api.ProcessTests.ProcessTests
{
    using System;

    public class ProcessTestSettings
    {
        public TimeSpan TokenExpiration { get; set; }

        public bool RunLoggingInUserProcessTest { get; set; }

        public bool RunUpdatingUserEmailProcessTest { get; set; }
    }
}
