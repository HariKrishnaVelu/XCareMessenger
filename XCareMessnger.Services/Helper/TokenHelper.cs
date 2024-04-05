using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XCareMessenger.Domain;

namespace XCareMessnger.Services.Helper
{
    public class TokenHelper
    {
        public TokenHelper() 
        {
            
        }
        public (string,string) CreateToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            var refreshtoken = Guid.NewGuid().ToString();
            return (tokenString,refreshtoken);
        }

        public bool DisableToken(string RefreshToken)
        {
            return false;
        }

        public bool DisableAllToken(string UserID)
        {
            return false;
        }
    }
}
