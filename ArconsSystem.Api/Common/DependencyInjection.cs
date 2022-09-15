using System.Text;
using ArconsSystem.Api.Common.Policies;
using ArconsSystem.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ArconsSystem.Api.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddAuth(
        this IServiceCollection services
    )
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters{
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Samir Nabil",
            ValidAudience = "Samir Nabil",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret-key-Samir"))
        });
        services.AddSingleton<IAuthorizationPolicyProvider, UserPermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, UserPermissionHandler>();
        // services.AddAuthorization(options => {
        //     options.AddPolicy("MakeRoles", policy =>
        //         policy.Requirements.Add(new UserPermission("MakeRoles")));
        // });
        return services;
    }
}