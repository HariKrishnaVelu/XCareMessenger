using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XCareMessenger.Domain;
using XCareMessnger.Services.DbConfig;
using XCareMessnger.Services.Interfaces;

namespace XCareMessnger.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly XChatContext _xChatContext;
        private readonly IConfiguration _configuration;
        public TokenService(XChatContext xChatContext, IConfiguration configuration)
        {
            _xChatContext = xChatContext;
            _configuration = configuration;
        }

        public  bool IsRefreshTokenValid(string userid, string RefreshToken)
        {
            var userToken = _xChatContext.userTokens.Where(u => u.UserMailID == userid && u.RefreshToken == RefreshToken 
                && u.IsActive==true && u.ExpiryDate>=DateTime.UtcNow).FirstOrDefault();
            return userToken != null;
        }

        public (string, string) CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName+" " +user.LastName),
                new Claim(ClaimTypes.Gender, user.Gender.ToString()),                
                new Claim(ClaimTypes.Email, user.UserMailID)                
            };
            return GenerateToken(claims, user.UserMailID);           
        }     

        private (string, string) GenerateToken(List<Claim> claims, string UserID)
        {
            var config = _configuration.GetSection("JWT");
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("SecretKey")));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: config.GetValue<string>("Issuer"),
                audience: config.GetValue<string>("Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(config.GetValue<int>("TokenExpiryTime")),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            var refreshToken = Guid.NewGuid().ToString();

            bool result = SaveToken(UserID, refreshToken);
            return result ? (tokenString, refreshToken) : ("", "");
        }

        public  bool DisableAllToken(string UserID,string RefreshToken)
        {
            var usertokens= _xChatContext.userTokens.Where(x => x.UserMailID == UserID && x.RefreshToken!= RefreshToken);
            foreach (var usertoken in usertokens)
            {
                usertoken.IsActive = false;
            }
            _xChatContext.userTokens.RemoveRange(usertokens);
            return  _xChatContext.SaveChanges()>0? true: false;
        }

        public  bool DisableToken(string RefreshToken)
        {
            var token =_xChatContext.userTokens.Where(x => x.RefreshToken == RefreshToken).FirstOrDefault();
            if(token!=null)
            {
                token.IsActive= true;
                _xChatContext.userTokens.Remove(token);
                return  _xChatContext.SaveChanges()>0? true:false;
            }
            return false;
        }

        public bool SaveToken(string userid, string RefreshToken)
        {
            UserToken userToken = new UserToken
            {
                UserMailID = userid,
                RefreshToken = RefreshToken,
                ExpiryDate = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:RefreshTokenExpiryTime"))
            };
            _xChatContext.userTokens.Add(userToken);
            return _xChatContext.SaveChanges()>0?true:false;
        }
    }
}
