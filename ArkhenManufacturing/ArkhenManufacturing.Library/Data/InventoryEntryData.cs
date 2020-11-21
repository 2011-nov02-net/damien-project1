using System;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an InventoryEntry
    /// </summary>
    public class InventoryEntryData : IData
    {
        private decimal _price;
        private decimal _discount;

        /// <summary>
        /// The id of the product in this entry
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// The location of the store in which this product resides
        /// </summary>
        public Guid LocationId { get; set; }

        /// <summary>
        /// The price per unit of the items
        /// </summary>
        public decimal Price
        {
            get => _price;
            set => _price = value > 0 ? value : _price;
        }

        /// <summary>
        /// The discount (as a percentage of 0-100) 
        /// </summary>
        public decimal Discount
        {
            get => _discount;
            set => _discount = (0M < value && value <= 100M)
                ? value : _discount;
        }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public InventoryEntryData() { }

        /// <summary>
        /// Constructor that allows for the internal data to be set
        /// </summary>
        /// <param name="productId">The id of the product in this entry</param>
        /// <param name="locationId">The location of the store in which this product resides</param>
        /// <param name="price">The price per unit of the items</param>
        /// <param name="discount">The discount (as a percentage of 0-100) </param>
        public InventoryEntryData(Guid productId, Guid locationId, decimal price, decimal discount) {
            ProductId = productId;
            LocationId = locationId;
            Price = price;
            Discount = discount;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">The other instance with the data being assigned to this</param>
        public InventoryEntryData(InventoryEntryData other) :
            this(other.ProductId, other.LocationId, other.Price, other.Discount) {
        }
    }
}
