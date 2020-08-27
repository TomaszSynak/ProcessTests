namespace ProcessTests.Api.Components.Authenticate
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Models;

    public class AuthService
    {
        private readonly JwtSettings _jwtSettings;

        private readonly UserProvider _userProvider;

        public AuthService(IOptions<JwtSettings> jwtOptions, UserProvider userProvider)
        {
            _userProvider = userProvider;
            _jwtSettings = jwtOptions.Value;
        }

        public static SymmetricSecurityKey GetSymmetricKey(string jwtSecret) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

        public virtual async Task<JwtTokenDto> GetJwtToken(CredentialsDto credentialsDto, CancellationToken cancellationToken = default)
        {
            var user = await _userProvider.GetUser(credentialsDto, cancellationToken);

            if (user == null)
            {
                return null;
            }

            var expirationTime = DateTime.UtcNow.AddDays(2);

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtTokenDescription = GetSecurityTokenDescriptor(user.GetClaimIdentity(), expirationTime);
            var jwtToken = jwtTokenHandler.CreateToken(jwtTokenDescription);

            return new JwtTokenDto
            {
                Token = jwtTokenHandler.WriteToken(jwtToken),
                ExpirationTime = expirationTime,
            };
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(ClaimsIdentity claimsIdentity, DateTime expirationTime)
        {
            return new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = expirationTime,
                Issuer = _jwtSettings.JwtIssuer,
                Audience = _jwtSettings.JwtAudience,
                NotBefore = DateTime.UtcNow,
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = GetSigningCredentials(),
            };
        }

        private SigningCredentials GetSigningCredentials() => new SigningCredentials(GetSymmetricKey(_jwtSettings.JwtSecret), SecurityAlgorithms.HmacSha512Signature);
    }
}
