using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProniaOnionAPİ.Application.Abstractions.Services;
using ProniaOnionAPİ.Application.DTOs.Tokens;
using ProniaOnionAPİ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Infrastructure.Implementations.Services
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly IConfiguration _configuration;

        public JwtTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResponseDto CreateToken(AppUser user, int minutes)
        {
            ICollection<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Surname,user.Surname)

            };
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                claims: claims,
                signingCredentials: credentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(handler.WriteToken(token) ,user.UserName,token.ValidTo);
        }
    }
}
