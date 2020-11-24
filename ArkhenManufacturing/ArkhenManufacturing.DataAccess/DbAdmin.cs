using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class DbAdmin
    {
        public DbAdmin() {
            LocationAdmins = new HashSet<DbLocationAdmin>();
            Orders = new HashSet<DbOrder>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<DbOrder> Orders { get; set; }
        public virtual ICollection<DbLocationAdmin> LocationAdmins { get; set; }
    }
}
