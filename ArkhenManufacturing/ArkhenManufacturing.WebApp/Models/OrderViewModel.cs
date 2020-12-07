using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArkhenManufacturing.WebApp.Models
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Admin Name")]
        public string AdminName { get; set; }

        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        public List<OrderLineViewModel> OrderLineViewModels { get; }

        public DateTime PlacementDate { get; set; }

        public OrderViewModel() { }

        public OrderViewModel(Guid orderId, string customerName, string adminName, string locationName, List<OrderLineViewModel> orderLineViewModels) {
            OrderId = orderId;
            CustomerName = customerName;
            AdminName = adminName;
            LocationName = locationName;
            OrderLineViewModels = orderLineViewModels;
        }
    }
}
