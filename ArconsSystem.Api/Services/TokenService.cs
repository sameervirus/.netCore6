using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ArconsSystem.Api.Models.Users;
using Microsoft.IdentityModel.Tokens;

namespace ArconsSystem.Api.Services;

public interface ITokenService
{
    public string GenerateToken(User user, string role);
}

public class TokenService : ITokenService
{
    public string GenerateToken(User user, string role)
    {
        var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret-key-Samir")), SecurityAlgorithms.HmacSha512);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
            new Claim(ClaimTypes.AuthorizationDecision, role),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        };

        var SecurityToken = new JwtSecurityToken(
            issuer: "Samir Nabil",
            audience: "Samir Nabil",
            expires: DateTime.Now.AddDays(1),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(SecurityToken);
    }
}