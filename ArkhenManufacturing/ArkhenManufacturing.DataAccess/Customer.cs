using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class Customer
    {
        public Customer() {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public DateTime SignUpDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid DefaultLocationId { get; set; }
        public Location DefaultLocation { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
