namespace EventAPI.UI.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
