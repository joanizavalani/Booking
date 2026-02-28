using Booking.Application.Contracts.Security;
using Booking.Domain.UserRoles;
using Booking.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking.Infrastructure.Contracts.Security;

public class TokenService
    : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public TokenResult GenerateToken(User user, List<UserRole> userRoles)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        foreach (var userRole in userRoles)
            claims.Add(
                new Claim(
                    ClaimTypes.Role,
                    userRole.Role.Name));

        var expiresAt =
            DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        var accessToken = tokenHandler.WriteToken(token);

        return new TokenResult(
            AccessToken: accessToken,
            ExpiresAt: expiresAt
        );
    }
}