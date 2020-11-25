using System;
using ArkhenManufacturing.Library.Data;

namespace ArkhenManufacturing.WebApp.Models
{
    public class InventoryEntryViewModel
    {
        public Guid ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Count { get; set; }
        public int Threshold { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }

        public InventoryEntryViewModel()
        {
        }

        public InventoryEntryViewModel(InventoryEntryData data)
        {
            
        }

        public static explicit operator InventoryEntryData(InventoryEntryViewModel viewModel)
        {
            return new InventoryEntryData
            {
                ProductId = viewModel.ProductId,
                
            };
        }
    }
}
