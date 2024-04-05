using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCareMessenger.Domain;

namespace XCareMessnger.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<User> GetUsers();
        Task<User> GetUser(string userid);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> UpdatePassword(string userid, string oldPassword, string NewPassword);
        Task<bool> DeleteUser(string userid);
    }
}
