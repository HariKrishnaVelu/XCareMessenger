using System.ComponentModel.DataAnnotations;

namespace XCareMessenger.API.Dtos.Account.Request
{
    public class LoginDto
    {
        [EmailAddress(ErrorMessage ="Invalid user id")]
        public string UserID { get; set; }
        public string Password { get; set; }
    }
}
