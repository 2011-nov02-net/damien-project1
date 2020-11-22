using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class Location
    {
        public Location() {
            Orders = new HashSet<Order>();
            Admins = new HashSet<Admin>();
            InventoryEntries = new HashSet<InventoryEntry>();
        }

        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<InventoryEntry> InventoryEntries { get; set; }

    }
}
