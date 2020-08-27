namespace ProcessTests.Api.Components.Authenticate
{
    using System;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Models;

    internal static class AuthenticateIoC
    {
        public static IServiceCollection ConfigureAuthenticate(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));

            serviceCollection
                .AddSingleton<UserProvider>();

            serviceCollection
                .Configure<JwtSettings>(jwtSettingsSection)
                .AddTransient<AuthService>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("JwtAuth", options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = jwtSettingsSection.Get<JwtSettings>().JwtAudience,
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettingsSection.Get<JwtSettings>().JwtIssuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthService.GetSymmetricKey(jwtSettingsSection.Get<JwtSettings>().JwtSecret),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(3)
                    };
                });

            return serviceCollection;
        }
    }
}
