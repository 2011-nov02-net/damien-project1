using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class Admin
    {
        public Admin() {
            Locations = new HashSet<Location>();
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
