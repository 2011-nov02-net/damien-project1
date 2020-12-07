using System.ComponentModel.DataAnnotations;

using ArkhenManufacturing.Library.Data;

namespace ArkhenManufacturing.WebApp.Models
{
    public class OrderLineViewModel
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public OrderLineViewModel() { }

        public OrderLineViewModel(string productName, OrderLineData data) {
            ProductName = productName;
            Count = data.Count;
            Price = data.PricePerUnit;
            Discount = data.Discount;
            TotalPrice = data.TotalPrice;
        }
    }
}
