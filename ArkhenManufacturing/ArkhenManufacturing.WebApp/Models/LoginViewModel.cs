using System.ComponentModel.DataAnnotations;

using ArkhenManufacturing.WebApp.Misc;

namespace ArkhenManufacturing.WebApp.Models
{
    public class LoginViewModel
    {
        [RegularExpression(RegularExpressions.NoSpecialCharacters,
                           ErrorMessage = ErrorMessages.NoSpecialCharacters)]
        [Required]
        public string Username { get; set; }

        [RegularExpression(RegularExpressions.NoSpecialCharacters,
                           ErrorMessage = ErrorMessages.NoSpecialCharacters)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }

        public LoginViewModel() { }

        public LoginViewModel(string username, string password, bool rememberMe)
        {
            Username = username;
            Password = password;
            RememberMe = rememberMe;
        }
    }
}
