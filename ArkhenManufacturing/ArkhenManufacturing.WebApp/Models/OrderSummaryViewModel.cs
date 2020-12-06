using System;

namespace ArkhenManufacturing.WebApp.Models
{
    public class OrderSummaryViewModel
    {
        public Tuple<string, Guid> CustomerLink { get; set; }
        public Tuple<string, Guid> AdminLink { get; set; }
        public Tuple<string, Guid> LocationLink { get; set; }
        public decimal Total { get; set; }
        public DateTime PlacementDate { get; set; }

        public OrderSummaryViewModel() { }
    }
}
