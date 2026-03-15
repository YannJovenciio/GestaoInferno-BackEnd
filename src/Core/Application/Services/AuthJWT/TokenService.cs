using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Inferno.src.Core.Domain.Entities;
using Inferno.src.Core.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Inferno.src.Core.Application.Services.AuthJWT;

public interface ITokenService
{
    string GenerateToken(Demon user, Client client, SigninKey signingKey);
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Demon user, Client client, SigninKey signingKey)
    {
        if (signingKey == null)
        {
            throw new InvalidOperationException("No active signing key available.");
        }

        var privateKeyBytes = Convert.FromBase64String(signingKey.PrivateKey);
        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

        var rsaSecurityKey = new RsaSecurityKey(rsa) { KeyId = signingKey.KeyId };

        var creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.IdDemon.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.DemonName),
            new Claim(ClaimTypes.NameIdentifier, user.DemonEmail),
            new Claim(ClaimTypes.Email, user.DemonEmail),
        };

        if (user.DemonRoles != null)
        {
            foreach (var userRole in user.DemonRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }
        }

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: client.ClientURL,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenDescriptor);
    }
}
