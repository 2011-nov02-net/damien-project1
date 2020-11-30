using System.ComponentModel.DataAnnotations;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.WebApp.Misc;

namespace ArkhenManufacturing.WebApp.Models
{
    public class RegisterViewModel
    {
        [RegularExpression(RegularExpressions.UserNameCharacters,
                           ErrorMessage = ErrorMessages.NameCharacters)]
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Account Password")]
        [Required]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public RegisterViewModel() { }

        public RegisterViewModel(string username, string password) {
            Username = username;
            Password = password;
        }
    }
}
