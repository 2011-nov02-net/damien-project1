using System;
using System.Collections.Generic;

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
        public List<Guid> LocationIdsAssignedTo { get; set; }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public AdminData() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationIdsAssignedTo">Locations in which this Admin can access and can monitor</param>
        /// <param name="firstName">First Name of the User</param>
        /// <param name="lastName">Last Name of the User</param>
        /// <param name="userName">UserName of the User</param>
        /// <param name="password">User's password</param>
        /// <param name="email">Email address of the user</param>
        public AdminData(List<Guid> locationIdsAssignedTo, string firstName, string lastName, string userName, string password, string email) :
            base(firstName, lastName, userName, password, email) {
            LocationIdsAssignedTo = locationIdsAssignedTo;
        }
    }
}
