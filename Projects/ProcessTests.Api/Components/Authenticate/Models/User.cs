namespace ProcessTests.Api.Components.Authenticate.Models
{
    using System;
    using System.Security.Claims;
    using Microsoft.IdentityModel.JsonWebTokens;

    public class User
    {
        public User(string name, string email, string password, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string Email { get; }

        public string Password { get; }

        public ClaimsIdentity GetClaimIdentity()
        {
            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, Email),
                new Claim("UserId", Id.ToString("D")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            });
        }
    }
}
