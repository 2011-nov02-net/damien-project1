using ArkhenManufacturing.Library.Extensions;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of a User
    /// </summary>
    public abstract class UserData : IData
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
        /// UserName of the User
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public UserData() { }

        /// <summary>
        /// Constructor of a User that allows calling code to fill out its data
        /// </summary>
        /// <param name="firstName">First Name of the User</param>
        /// <param name="lastName">Last Name of the User</param>
        /// <param name="userName">UserName of the User</param>
        /// <param name="password">User's password</param>
        /// <param name="email">Email address of the user</param>
        public UserData(string firstName, string lastName, string userName, string password, string email) {
            firstName.NullOrEmptyCheck(nameof(firstName));
            lastName.NullOrEmptyCheck(nameof(lastName));
            userName.NullOrEmptyCheck(nameof(userName));
            password.NullOrEmptyCheck(nameof(password));
            email.NullOrEmptyCheck(nameof(email));

            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
            Email = email;
        }
    }
}
