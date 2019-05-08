using AltaRail.Application.DTOs;
using AltaRail.Application.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace AltaRail.Application
{
    public class TokenService : ITokenService
    {
        public string CreateToken(CreateTokenDto createToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(createToken.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(createToken.Issuer,
              createToken.Issuer,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
