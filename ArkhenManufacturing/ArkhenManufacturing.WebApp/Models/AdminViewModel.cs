using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Misc;

namespace ArkhenManufacturing.WebApp.Models
{
    public class AdminViewModel
    {
        [RegularExpression(RegularExpressions.NameCharacters,
            ErrorMessage = ErrorMessages.NameCharacters)]
        [Required]
        public string FirstName { get; set; }
        
        [RegularExpression(RegularExpressions.NameCharacters,
            ErrorMessage = ErrorMessages.NameCharacters)]
        [Required]
        public string LastName { get; set; }
        
        [RegularExpression(RegularExpressions.UserNameCharacters,
            ErrorMessage = ErrorMessages.NameCharacters)]
        [Required]
        public string UserName { get; set; }
        
        [RegularExpression(RegularExpressions.PasswordCharacters,
            ErrorMessage = ErrorMessages.PasswordCharacters)]
        [Required]
        public string Password { get; set; }

        [RegularExpression(RegularExpressions.EmailCharacters,
            ErrorMessage = ErrorMessages.EmailCharacters)]
        [Required]
        public string Email { get; set; }
        public List<Location> Locations { get; set; }

        public AdminViewModel(AdminData data) {
            FirstName = data.FirstName;
            LastName = data.LastName;
            UserName = data.UserName;
            Password = data.Password;
            Email = data.Password;
            Locations = data.LocationIds
                .ConvertAll(id => ArchivistInterface.Retrieve<Location>(id));
        }

        public static explicit operator AdminData(AdminViewModel viewModel) {
            var locationIds = viewModel.Locations
                .ConvertAll(l => l.Id);
            return new AdminData(
                viewModel.FirstName, 
                viewModel.LastName, 
                viewModel.UserName, 
                viewModel.Password, 
                viewModel.Email, 
                locationIds
            );
        }
    }
}
