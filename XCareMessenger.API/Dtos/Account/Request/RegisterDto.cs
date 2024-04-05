using System.ComponentModel.DataAnnotations;

namespace XCareMessenger.API.Dtos.Account.Request
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid user id")]
        public string UserID { get; set; }
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Password not match")]
        public string ConfirmPassword { get; set; }
    }
}
