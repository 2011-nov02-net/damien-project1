using ArkhenManufacturing.Library.Data;

namespace ArkhenManufacturing.WebApp.Models
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public int Count { get; set; }
        public int Threshold { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }

        public ProductViewModel() { }

        public ProductViewModel(string productName, InventoryEntryData data) {
            ProductName = productName;
            Count = data.Count;
            Threshold = data.Threshold;
            Price = data.Price;
            Discount = data.Discount;
        }

        public static explicit operator InventoryEntryData(ProductViewModel viewModel) {
            decimal discount = viewModel.Discount ?? 0.0M;
            return new InventoryEntryData
            {
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
