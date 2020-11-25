namespace ArkhenManufacturing.WebApp.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        
        public LoginViewModel(string username, string password, bool rememberMe)
        {
            Username = username;
            Password = password;
            RememberMe = rememberMe;
        }
    }
}
