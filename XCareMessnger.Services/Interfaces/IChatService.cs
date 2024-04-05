using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCareMessenger.Domain;

namespace XCareMessnger.Services.Interfaces
{
    public interface IChatService
    {
        Task<UserMessage> GetMessage();
        Task<bool> SendMessage(UserMessage message);
        
    }
}
