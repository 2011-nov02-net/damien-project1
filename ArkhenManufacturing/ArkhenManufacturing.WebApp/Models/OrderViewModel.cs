using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.WebApp.Models
{
    public class OrderViewModel
    {
        public string CustomerName { get; set; }
        public string AdminName { get; set; }
        public string LocationName { get; set; }
        public List<OrderLineViewModel> OrderLineViewModels { get; }
        public DateTime PlacementDate { get; set; }

        // TODO: Add in the partial rendering of the products in the order (constrained to a scrollbox)
        public OrderViewModel() { }

        public OrderViewModel(string customerName, string adminName, string locationName, List<OrderLineViewModel> orderLineViewModels) {
            CustomerName = customerName;
            AdminName = adminName;
            LocationName = locationName;
            OrderLineViewModels = orderLineViewModels;
        }
    }
}
