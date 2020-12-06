using System;

using ArkhenManufacturing.Library.Data;

namespace ArkhenManufacturing.WebApp.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public int Threshold { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public decimal DiscountedPrice
        {
            get
            {
                return (1.0M - Discount / 100.0M) * Price;
            }
        }

        public decimal DiscountPercentage { get => Discount; }

        public ProductViewModel() { }

        public ProductViewModel(string productName, InventoryEntryData data) {
            Id = data.ProductId;
            ProductName = productName;
            Count = data.Count;
            Threshold = data.Threshold;
            Price = data.Price;
            Discount = data.Discount;
        }

        public static explicit operator InventoryEntryData(ProductViewModel viewModel) {
            decimal discount = viewModel.Discount;
            return new InventoryEntryData
            {
                ProductId = viewModel.Id,
                Count = viewModel.Count,
                Price = viewModel.Price,
                Discount = discount
            };
        }

        public static explicit operator ProductData(ProductViewModel viewModel) {
            return new ProductData(viewModel.ProductName);
        }
    }
}
