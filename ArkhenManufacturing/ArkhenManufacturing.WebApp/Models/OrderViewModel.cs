using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArkhenManufacturing.WebApp.Models
{
    public class OrderViewModel
    {
        public Guid CustomerId { get; set; }
        public Guid AdminId { get; set; }
        public Guid LocationId { get; set; }
        public DateTime PlacementDate { get; set; }

        // TODO: Add in the partial rendering of the products in the order (constrained to a scrollbox)
        public OrderViewModel() { }
    }
}
