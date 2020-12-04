using System;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an Admin
    ///     (that is not contained in the UserData parent class)
    /// </summary>
    public class AdminData : UserData
    {
        /// <summary>
        /// Locations in which this Admin can access and can monitor
        /// </summary>
        public Guid LocationId { get; set; }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public AdminData() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationIds">Locations in which this Admin can access and can monitor</param>
        /// <param name="firstName">First Name of the User</param>
        /// <param name="lastName">Last Name of the User</param>
        /// <param name="email">Email address of the user</param>
        public AdminData(string firstName, string lastName, string email, Guid locationId) :
            base(firstName, lastName, email) {
            LocationId = locationId;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">The value being assigned to this</param>
        public AdminData(AdminData other) :
            this(other.FirstName, other.LastName, other.Email, other.LocationId) {
        }
    }
}
