using System;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an InventoryEntry
    /// </summary>
    public class InventoryEntryData : IData
    {
        private decimal _price;
        private double _discount;

        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }

        public decimal Price
        {
            get => _price;
            set => _price = value > 0 ? value : _price;
        }
        public double Discount
        {
            get => _discount;
            set => _discount = (0 < value && value <= 100)
                ? value : _discount;
        }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public InventoryEntryData() { }

        public InventoryEntryData(Guid productId, Guid locationId, decimal price, double discount) {
            ProductId = productId;
            LocationId = locationId;
            Price = price;
            Discount = discount;
        }
    }
}
