using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCareMessenger.Domain;
using XCareMessnger.Services.DbConfig;
using XCareMessnger.Services.Interfaces;

namespace XCareMessnger.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly XChatContext _xChatContext;
        public UserService(XChatContext xChatContext) 
        {
            _xChatContext = xChatContext;
        }
        public async Task<bool> AddUser(User user)
        {
            _xChatContext.users.Add(user);
            return await _xChatContext.SaveChangesAsync()>0;
        }

        public async Task<bool> DeleteUser(string userid)
        {
            var user= await GetUsers().Where(u=>u.UserMailID==userid).FirstOrDefaultAsync();
            if(user!=null)
            {
                _xChatContext.users.Remove(user);
                return await _xChatContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<User> GetUser(string userid)
        {
            var user =await GetUsers().Where(u => u.UserMailID == userid).FirstOrDefaultAsync();
            return user;
        }
        public IQueryable<User> GetUsers()
        {
            return _xChatContext.users;
        }
        public async Task<bool> UpdatePassword(string userid, string oldPassword, string NewPassword)
        {
            var user = await GetUsers().Where(u => u.UserMailID == userid && u.Password==oldPassword).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Password = NewPassword;
                _xChatContext.users.Update(user);
                return await _xChatContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> UpdateUser(User userModel)
        {
            var user = await GetUsers().Where(u => u.UserMailID == userModel.UserMailID).FirstOrDefaultAsync();
            if (user != null)
            {                
                _xChatContext.users.Update(userModel);
                return await _xChatContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
