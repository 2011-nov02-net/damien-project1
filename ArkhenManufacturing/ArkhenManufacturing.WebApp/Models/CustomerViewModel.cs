using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Extensions;
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

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

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

        [Display(Name = "Zip Code")]
        [Required, DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        public Guid? DefaultLocationId { get; set; }

        public List<Tuple<string,Guid>> Locations { get; set; }

        public CustomerViewModel() { }

        /// <summary>
        /// Create a customer view model from the provided customer and address;
        ///     (it will use the data components from them)
        /// </summary>
        /// <param name="customer">The customer data being stored</param>
        /// <param name="address">The address data being stored</param>
        /// <exception cref="ArgumentException">Exception thrown when the data of the customer is null</exception>
        public CustomerViewModel(Customer customer, Address address, List<Tuple<string, Guid>> locations) {
            customer.NullCheck(nameof(customer));
            address.NullCheck(nameof(address));

            Locations = locations;

            var customerData = customer.GetData() as CustomerData;
            var addressData = address.GetData() as AddressData;

            FirstName = customerData.FirstName;
            LastName = customerData.LastName;
            PhoneNumber = customerData.PhoneNumber;
            Email = customerData.Email;
            DefaultLocationId = customerData.DefaultLocationId;

            Line1 = addressData.Line1;
            Line2 = addressData.Line2;
            City = addressData.City;
            State = addressData.State;
            Country = addressData.Country;
            ZipCode = addressData.ZipCode;
        }

        public static explicit operator CustomerData(CustomerViewModel viewModel) {
            return new CustomerData
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email
            };
        }

        public static explicit operator AddressData(CustomerViewModel viewModel) {
            return new AddressData
            {
                Line1 = viewModel.Line1,
                Line2 = viewModel.Line2,
                City = viewModel.City,
                State = viewModel.State,
                Country = viewModel.Country,
                ZipCode = viewModel.ZipCode
            };
        }
    }
}
