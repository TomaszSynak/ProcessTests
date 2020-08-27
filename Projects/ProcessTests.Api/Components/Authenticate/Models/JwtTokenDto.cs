namespace ProcessTests.Api.Components.Authenticate.Models
{
    using System;

    public class JwtTokenDto
    {
        public string Token { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
