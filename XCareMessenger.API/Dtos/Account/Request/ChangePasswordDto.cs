namespace XCareMessenger.API.Dtos.Account.Request
{
    public class ChangePasswordDto
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
