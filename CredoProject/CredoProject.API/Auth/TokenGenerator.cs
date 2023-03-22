﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace CredoProject.API.Auth
{
    public class TokenGenerator
    {
        private readonly JwtSettings _settings;

        public TokenGenerator(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public string Generate(int id, string str)
        {
            var claims = new List<Claim>
            {
                //new Claim(new ClaimsIdentityOptions().UserIdClaimType, id),
                new Claim(new ClaimsIdentityOptions().UserIdClaimType, id.ToString()),
                //new Claim("email", email),
                new Claim(ClaimTypes.Role, str),
                new Claim("test type", "test value")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecrectKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: credentials);

            var tokenGenerator = new JwtSecurityTokenHandler();
            var jwtString = tokenGenerator.WriteToken(token);
            return jwtString;
            //return "";
        }
    }
}