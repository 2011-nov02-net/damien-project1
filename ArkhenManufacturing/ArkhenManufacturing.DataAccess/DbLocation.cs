using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class DbLocation
    {
        public DbLocation() {
            Orders = new HashSet<DbOrder>();
            LocationAdmins = new HashSet<DbLocationAdmin>();
            InventoryEntries = new HashSet<DbInventoryEntry>();
        }

        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public DbAddress Address { get; set; }

        public virtual ICollection<DbOrder> Orders { get; set; }
        public virtual ICollection<DbInventoryEntry> InventoryEntries { get; set; }
        public virtual ICollection<DbLocationAdmin> LocationAdmins { get; set; }

    }
}
