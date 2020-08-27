namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    internal static class ApiEndpoints
    {
        public static string CreateAuthToken() => "api/auth";

        public static string CheckAuthToken() => "api/auth/check";

        public static string UpdateUserEmail() => "api/auth/user/email";
    }
}
