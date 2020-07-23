using Binit.Framework.AbstractEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WebAPITools
{
    public class TokenManager
    {
        private readonly IConfiguration configuration;

        public TokenManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(UserEntity user, ICollection<string> roles, Claim tenantClaim)
        {
            var jwtSettings = this.configuration.GetSection("JwtSettings");

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            // Define user claims.
            var claims = new List<Claim>();
            claims.Add(tenantClaim);
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());
            claims.Add(new Claim(ClaimTypes.Name, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create jwt token.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Write jwt token to string.
            return tokenHandler.WriteToken(token);
        }
    }
}