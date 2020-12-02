using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class DbLocation : DbEntity
    {
        public DbLocation() {
            Orders = new HashSet<DbOrder>();
            Admins = new HashSet<DbAdmin>();
            InventoryEntries = new HashSet<DbInventoryEntry>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public DbAddress Address { get; set; }

        public virtual ICollection<DbOrder> Orders { get; set; }
        public virtual ICollection<DbInventoryEntry> InventoryEntries { get; set; }
        public virtual ICollection<DbAdmin> Admins { get; set; }

    }
}
