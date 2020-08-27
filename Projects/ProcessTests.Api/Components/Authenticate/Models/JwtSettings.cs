namespace ProcessTests.Api.Components.Authenticate.Models
{
    public class JwtSettings
    {
        public string JwtSecret { get; set; }

        public string JwtIssuer { get; set; }

        public string JwtAudience { get; set; }
    }
}
