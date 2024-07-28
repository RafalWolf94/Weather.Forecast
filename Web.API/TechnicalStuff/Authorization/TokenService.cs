using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Domain.Authorization;
using Core.Domain.Models.AppUsers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Weather.Forecast.TechnicalStuff.Authorization;

public class TokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
{
    private readonly JwtSettings jwtSettings = jwtSettings.Value;

    public string GetNewJwtTokenFor(AppUser user)
    {
        var jwtClaims = CreateJwtClaims(user);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, jwtClaims,
            expires: DateTime.UtcNow.AddYears(10),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static IEnumerable<Claim> CreateJwtClaims(AppUser user)
    {
        return new List<Claim>
        {
            new("uid", user.Id.ToString()),
            // new(JwtRegisteredClaimNames.Sub, user.Email.Value),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
    }
}