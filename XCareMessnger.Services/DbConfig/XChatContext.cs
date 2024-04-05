using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XCareMessenger.Domain;

namespace XCareMessnger.Services.DbConfig
{
    public class XChatContext : DbContext
    {       
        public XChatContext(DbContextOptions<XChatContext> options): base(options)
        {
            
        }
        public DbSet<User> users { get; set; }
        public DbSet<UserMessage> userMessages  { get; set; }
        public DbSet<UserToken> userTokens { get; set; }
    }
}
