using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an Order
    /// </summary>
    public class OrderData : IData
    {
        /// <summary>
        /// Id of the customer that made this order
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The admin assigned to monitor and assist with this order
        /// </summary>
        public Guid AttendingAdminId { get; set; }

        /// <summary>
        /// The location in which the order was placed
        /// </summary>
        public Guid LocationId { get; set; }

        /// <summary>
        /// The date in which the order was placed
        /// </summary>
        public DateTime PlacementDate { get; set; }

        /// <summary>
        /// The overall total of the order
        /// </summary>
        public decimal Total { get; set; }

        // TODO: Decide what to do here for products and their prices here
        // OrderId, ProductId, Count, Price per Unit => OrderDetails?
        // \--------+-------/
        //          |
        //         have these two as a composite key for the DB?
        public IDictionary<Guid, uint> ProductIdsWithCount { get; set; }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public OrderData() { }

        public OrderData(Guid customerId, Guid locationId, DateTime placementDate, decimal total, IDictionary<Guid, uint> productIdsWithCount) {
            CustomerId = customerId;
            LocationId = locationId;
            PlacementDate = placementDate;
            Total = total;
            ProductIdsWithCount = productIdsWithCount;
        }
    }
}
