using System;
using ArkhenManufacturing.Library.Extensions;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an Customer
    ///     (that is not contained in the UserData parent class)
    /// </summary>
    public class CustomerData : NamedData
    {
        /// <summary>
        /// Property that only accesses the FirstName and LastName
        /// </summary>
        public string Fullname => $"{LastName}, {FirstName}";

        /// <summary>
        /// First Name of the User
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the User
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Customer's Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Customer's Address
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// Date in which the customer signed up
        /// </summary>
        public DateTime SignUpDate { get; set; }

        /// <summary>
        /// Date of birth with the customer
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Default store location that the customer shops from;
        ///     If null, implies they don't have a default
        /// </summary>
        public Guid? DefaultLocationId { get; set; }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public CustomerData() :
            base(nameof(CustomerData)) { }

        /// <summary>
        /// Constructor that allows the calling code to assign all of the values to it.
        /// </summary>
        /// <param name="phoneNumber">Customers' Phone Number</param>
        /// <param name="addressId">Guid id of the Customer's Address</param>
        /// <param name="signUpDate">Sign up date of the Customer</param>
        /// <param name="birthDate">Customer's birthday/time</param>
        /// <param name="defaultLocationId">Location that the Customer prefers to shop at</param>
        /// <param name="firstName">First Name of the User</param>
        /// <param name="lastName">Last Name of the User</param>
        /// <param name="username">UserName of the User</param>
        /// <param name="password">User's password</param>
        /// <param name="email">Email address of the user</param>
        public CustomerData(string firstName, string lastName, string email, string phoneNumber, Guid addressId, DateTime signUpDate, DateTime birthDate, Guid? defaultLocationId) :
            base($"{lastName}, {firstName}") {
            firstName.NullOrEmptyCheck(nameof(firstName));
            lastName.NullOrEmptyCheck(nameof(lastName));
            email.NullOrEmptyCheck(nameof(email));

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber?.Trim();
            AddressId = addressId;
            SignUpDate = signUpDate;
            BirthDate = birthDate;
            DefaultLocationId = defaultLocationId;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">The value being assigned to this</param>
        public CustomerData(CustomerData other) :
            this(other.FirstName, other.LastName, other.Email, other.PhoneNumber, other.AddressId, other.SignUpDate, other.BirthDate, other.DefaultLocationId) {
        }
    }
}
