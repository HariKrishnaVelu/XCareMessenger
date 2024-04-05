using Microsoft.EntityFrameworkCore;
using XCareMessenger.API.Hubs;
using XCareMessnger.Services.DbConfig;
using XCareMessnger.Services.Implementations;
using XCareMessnger.Services.Interfaces;

namespace XCareMessenger.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<XChatContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("XchatConnstr")));
            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<ChatHub>("/chatHub");
            app.MapControllers();

            app.Run();
        }
    }
}