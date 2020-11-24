using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.WebApp.Misc;

namespace ArkhenManufacturing.WebApp.Models
{
    public class AddressViewModel
    {
        [RegularExpression(RegularExpressions.NoSpecialCharacters, 
            ErrorMessage = ErrorMessages.NoSpecialCharacters)]
        [Display(Name = "Address Line 1")]
        [Required]
        public string Line1 { get; set; }
        
        [RegularExpression(RegularExpressions.NoSpecialCharacters, 
            ErrorMessage = ErrorMessages.NoSpecialCharacters)]
        [Display(Name = "Address Line 2")]
        public string Line2 { get; set; }

        [RegularExpression(RegularExpressions.NameCharacters,
            ErrorMessage = ErrorMessages.NameCharacters)]
        [Required]
        public string City { get; set; }
        
        [RegularExpression(RegularExpressions.NameCharacters,
            ErrorMessage = ErrorMessages.NameCharacters)]
        public string State { get; set; }
        
        [Required]
        [RegularExpression(RegularExpressions.NameCharacters,
            ErrorMessage = ErrorMessages.NameCharacters)]
        public string Country { get; set; }

        [RegularExpression(RegularExpressions.NumbersOnly, 
            ErrorMessage = ErrorMessages.NumbersOnly)]
        [Display(Name = "Zip Code")]
        [StringLength(5)]
        [Required]
        public string ZipCode { get; set; }

        /// <summary>
        /// This allows a ViewModel to be constructed from the data if 
        ///     the data is passed in
        /// </summary>
        /// <param name="data">The data of the address being shown</param>
        public AddressViewModel(AddressData data) {
            Line1 = data.Line1;
            Line2 = data.Line2;
            City = data.City;
            State = data.State;
            Country = data.Country;
            ZipCode = data.ZipCode;
        }

        /// <summary>
        /// Explicit operator to get the specified AddressViewModel
        ///     as its AddressData component
        /// </summary>
        /// <param name="viewModel"></param>
        public static explicit operator AddressData(AddressViewModel viewModel) {
            return new AddressData(
                viewModel.Line1, 
                viewModel.Line2, 
                viewModel.City, 
                viewModel.State, 
                viewModel.Country, 
                viewModel.ZipCode
            );
        }
    }
}
