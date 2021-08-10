using AuthorizationService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizationService.Repository
{
    public class TokenRepo : ITokenRepo
    {
        public string CreateJWT(IConfiguration config, User user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = new JwtSecurityToken(
                    config["Jwt:Issuer"],
                    config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
    }
}
