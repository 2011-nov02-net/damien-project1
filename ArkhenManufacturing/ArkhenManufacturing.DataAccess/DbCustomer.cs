using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class DbCustomer : DbEntity
    {
        public DbCustomer() {
            Orders = new HashSet<DbOrder>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid AddressId { get; set; }
        public DbAddress Address { get; set; }
        public DateTime SignUpDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid DefaultLocationId { get; set; }
        public DbLocation DefaultLocation { get; set; }

        public virtual ICollection<DbOrder> Orders { get; set; }
    }
}
