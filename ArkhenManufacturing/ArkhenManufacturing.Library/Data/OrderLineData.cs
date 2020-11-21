using System;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Represents an orderline within an order
    /// </summary>
    public class OrderLineData : IData
    {
        /// <summary>
        /// The id of the order this was placed to
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// The id of the product referenced
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// The count of items being added
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The total price per unit on the product
        /// </summary>
        public decimal PricePerUnit { get; set; }

        /// <summary>
        /// The discount that was applied on the products
        ///     It is the discount for the total price of the products,
        ///     not per any of the individual items
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Accessor for the total price of the orderline
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                var total = Count * PricePerUnit;
                var discount = (100M - Discount) / 100M;
                return total * discount;
            }
        }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public OrderLineData() { }

        /// <summary>
        /// Constructor that allows for the internal data to be set
        /// </summary>
        /// <param name="orderId">The id of the order this was placed to</param>
        /// <param name="productId">The id of the product referenced</param>
        /// <param name="count">The count of items being added</param>
        /// <param name="pricePerUnit">The total price per unit on the product</param>
        /// <param name="discount">The overall discount on the products as whole</param>
        public OrderLineData(Guid orderId, Guid productId, int count, decimal pricePerUnit, decimal discount) {
            OrderId = orderId;
            ProductId = productId;
            Count = count;
            PricePerUnit = pricePerUnit;
            Discount = discount;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">The other instance with the data being assigned to this</param>
        public OrderLineData(OrderLineData other) :
            this(other.OrderId, other.ProductId, other.Count, other.PricePerUnit, other.Discount) {
        }
    }
}
