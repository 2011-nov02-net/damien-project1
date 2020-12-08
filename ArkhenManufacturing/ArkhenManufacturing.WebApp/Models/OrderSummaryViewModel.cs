using System;
using System.ComponentModel.DataAnnotations;

namespace ArkhenManufacturing.WebApp.Models
{
    public class OrderSummaryViewModel
    {
        public Guid OrderId { get; set; }

        [Display(Name = "Customer")]
        public Tuple<string, Guid> CustomerLink { get; set; }

        [Display(Name = "Admin")]
        public Tuple<string, Guid> AdminLink { get; set; }

        [Display(Name = "Location")]
        public Tuple<string, Guid> LocationLink { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Total { get; set; }

        public DateTime PlacementDate { get; set; }

        public OrderSummaryViewModel() { }
    }
}
