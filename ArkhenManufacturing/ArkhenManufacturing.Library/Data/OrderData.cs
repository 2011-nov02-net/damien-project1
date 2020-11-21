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

        /// <summary>
        /// A collection of ids of the orderlines that were placed to this order
        /// </summary>
        public ICollection<Guid> OrderLineIds { get; set; }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public OrderData() { }

        /// <summary>
        /// Constructor that allows for the internal data to be set
        /// </summary>
        /// <param name="customerId">Id of the customer that made this order</param>
        /// <param name="attendingAdminId">The admin assigned to monitor and assist with this order</param>
        /// <param name="locationId">The location in which the order was placed</param>
        /// <param name="placementDate">The date in which the order was placed</param>
        /// <param name="total">The overall total of the order</param>
        /// <param name="orderLineIds">A collection of ids of the orderlines that were placed to this order</param>
        public OrderData(Guid customerId, Guid attendingAdminId, Guid locationId, DateTime placementDate, decimal total, ICollection<Guid> orderLineIds) {
            CustomerId = customerId;
            AttendingAdminId = attendingAdminId;
            LocationId = locationId;
            PlacementDate = placementDate;
            Total = total;
            OrderLineIds = orderLineIds;
        }
    }
}
