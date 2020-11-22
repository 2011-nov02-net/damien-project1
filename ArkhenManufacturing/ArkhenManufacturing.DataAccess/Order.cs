using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.DataAccess
{
    public class Order
    {
        public Order() {
            OrderLines = new HashSet<OrderLine>();
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid AdminId { get; set; }
        public Admin Admin { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public DateTime PlacementDate { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
