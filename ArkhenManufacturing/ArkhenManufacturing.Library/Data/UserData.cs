using ArkhenManufacturing.Library.Extensions;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of a User
    /// </summary>
    public class UserData : NamedData
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
        public string Username { get; set; }

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
        public UserData() : base("") { }

        /// <summary>
        /// Constructor of a User that allows calling code to fill out its data
        /// </summary>
        /// <param name="firstName">First Name of the User</param>
        /// <param name="lastName">Last Name of the User</param>
        /// <param name="username">UserName of the User</param>
        /// <param name="password">User's password</param>
        /// <param name="email">Email address of the user</param>
        public UserData(string firstName, string lastName, string username, string password, string email) :
            base($"{lastName}, {firstName}") {
            firstName.NullOrEmptyCheck(nameof(firstName));
            lastName.NullOrEmptyCheck(nameof(lastName));
            username.NullOrEmptyCheck(nameof(username));
            password.NullOrEmptyCheck(nameof(password));
            email.NullOrEmptyCheck(nameof(email));

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Username = username.Trim();
            Password = password.Trim();
            Email = email.Trim();
        }
    }
}
