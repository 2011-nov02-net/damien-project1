using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Misc;

namespace ArkhenManufacturing.WebApp.Models
{
    public class CustomerViewModel
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

        [RegularExpression(RegularExpressions.PhoneNumber,
                           ErrorMessage = ErrorMessages.PhoneNumber)]
        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public Guid AddressId { get; set; }
        public AddressViewModel Address { get; set; }
        
        public Guid? DefaultStoreLocationId { get; set; }

        /// <summary>
        /// Create a customer view model from the provided customer and address;
        ///     (it will use the data components from them)
        /// </summary>
        /// <param name="customer">The customer data being stored</param>
        /// <param name="address">The address data being stored</param>
        /// <exception cref="ArgumentException">Exception thrown when the data of the customer is null</exception>
        public CustomerViewModel(Customer customer, Address address)
        {
            if (!(customer.GetData() is CustomerData customerData))
            {
                throw new ArgumentException("CustomerData cannot be null");
            }

            FirstName = customerData.FirstName;
            LastName = customerData.LastName;
            Username = customerData.Username;
            Password = customerData.Password;
            Email = customerData.Email;
            AddressId = customerData.AddressId;
            DefaultStoreLocationId = customerData.DefaultLocationId;
            
            Address = new AddressViewModel(address);
        }

        public static explicit operator CustomerData(CustomerViewModel viewModel)
        {
            return new CustomerData
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Username = viewModel.LastName,
                Password = viewModel.LastName,
                Email = viewModel.Email,
                AddressId = viewModel.AddressId,
                DefaultLocationId = viewModel.DefaultStoreLocationId
            };
        }
    }
}
