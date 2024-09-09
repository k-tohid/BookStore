using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.DTO;
using BookStore.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public AuthenticationResponseDTO CreateJwtToken(ApplicationUser user)
        {
            var expirationDurtion = Convert.ToDouble(_configuration["Jwt:EXPIRATION_MIINUTES"]);
            var expirattionTime = DateTime.UtcNow.AddMinutes(expirationDurtion);

            Claim[] claims = new Claim[] {

                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenGenerator = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expirattionTime,
            signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponseDTO() { Token = token, UserName = user.UserName, Email = user.Email };


        }
    }
}
