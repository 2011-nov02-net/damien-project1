using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class DbOrder : DbEntity
    {
        public DbOrder() {
            OrderLines = new HashSet<DbOrderLine>();
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DbCustomer Customer { get; set; }
        public Guid AdminId { get; set; }
        public DbAdmin Admin { get; set; }
        public Guid LocationId { get; set; }
        public DbLocation Location { get; set; }
        public DateTime PlacementDate { get; set; }

        public virtual ICollection<DbOrderLine> OrderLines { get; set; }
    }
}
