using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class DbAdmin : DbEntity
    {
        public DbAdmin() {
            Orders = new HashSet<DbOrder>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid LocationId { get; set; }
        public DbLocation Location { get; set; }

        public virtual ICollection<DbOrder> Orders { get; set; }
    }
}
