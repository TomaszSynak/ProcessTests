namespace ProcessTests.Api.Infrastructure
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public static class HttpContextExtensions
    {
        public static string GetEmail(this HttpContext httpContext)
        {
            var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));

            if (claim == null)
            {
                throw new ArgumentException("Claim not found");
            }

            return claim.Value;
        }

        public static Guid GetUserId(this HttpContext httpContext)
        {
            var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("UserId"));

            if (claim == null)
            {
                throw new ArgumentException("Claim not found");
            }

            return Guid.Parse(claim.Value);
        }
    }
}
