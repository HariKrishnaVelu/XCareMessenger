using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XCareMessenger.Domain;

namespace XCareMessnger.Services.Interfaces
{
    public interface ITokenService
    {
        bool IsRefreshTokenValid(string userid, string RefreshToken);
        (string,string) CreateToken(User user);
        bool DisableToken(string RefreshToken);
        bool DisableAllToken(string UserID, string RefreshToken);
        bool SaveToken(string userid, string RefreshToken);
    }
}
