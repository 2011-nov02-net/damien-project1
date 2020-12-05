using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Misc;

namespace ArkhenManufacturing.WebApp.Models
{
    public class AdminViewModel
    {

        [RegularExpression(RegularExpressions.NameCharacters,
                           ErrorMessage = ErrorMessages.NameCharacters)]
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [RegularExpression(RegularExpressions.NameCharacters,
                           ErrorMessage = ErrorMessages.NameCharacters)]
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        [RegularExpression(RegularExpressions.UserNameCharacters,
                           ErrorMessage = ErrorMessages.NameCharacters)]
        [Required]
        public string Username { get; set; }

        [RegularExpression(RegularExpressions.PasswordCharacters,
                           ErrorMessage = ErrorMessages.PasswordCharacters)]
        [Required]
        public string Password { get; set; }

        [RegularExpression(RegularExpressions.EmailCharacters,
                           ErrorMessage = ErrorMessages.EmailCharacters)]
        [Required]
        public string Email { get; set; }

        public AdminViewModel(AdminData data) {
            FirstName = data.FirstName;
            LastName = data.LastName;
            Email = data.Email;
        }

        public static explicit operator AdminData(AdminViewModel viewModel) {
            return new AdminData
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };
        }
    }
}
