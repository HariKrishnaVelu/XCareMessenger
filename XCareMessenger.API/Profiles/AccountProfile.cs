using XCareMessenger.API.Dtos.Account.Request;
using XCareMessenger.API.Dtos.Account.Response;
using XCareMessenger.Domain;

namespace XCareMessenger.API.Profiles
{
    public class AccountProfile : AutoMapper.Profile
    {
        public AccountProfile() 
        {
            CreateMap<UserDto, User>();
            CreateMap<RegisterDto, User>().ReverseMap();
        }
    }
}
