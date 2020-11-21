using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an Location
    ///     (except for its name)
    /// </summary>
    public class LocationData : NamedData
    {
        /// <summary>
        /// Id of the address of the store location
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// A collection of order ids placed to this location
        /// </summary>
        public List<Guid> OrderIds { get; set; }

        /// <summary>
        /// A collection of the administrators of this location
        ///     that can view orders, edit stock, and the like
        /// </summary>
        public List<Guid> AdminIds { get; set; }

        /// <summary>
        /// The inventory of this store location
        ///     It holds the ids of the InventoryEntries, 
        ///     and they store the actual product information
        /// </summary>
        public List<Guid> InventoryEntryIds { get; set; }

        /// <summary>
        /// Default constructor that sets the NamedData's parameter as the string passed in
        /// </summary>
        /// <param name="name">The name of the data</param>
        public LocationData(string name) :
            base(name) {
            OrderIds = new List<Guid>();
        }

        /// <summary>
        /// Constructor that sets all of the internal data
        /// </summary>
        /// <param name="name">Name of the data</param>
        /// <param name="addressId">Id of the address of the store location</param>
        /// <param name="orderIds">A collection of order ids placed to this location</param>
        /// <param name="adminIds">A collection of the administrators of this location</param>
        /// <param name="inventoryEntryIds">The inventory entries of this store location</param>
        public LocationData(string name, Guid addressId, List<Guid> orderIds, List<Guid> adminIds, List<Guid> inventoryEntryIds) :
            base(name) {
            AddressId = addressId;
            OrderIds = orderIds;
            AdminIds = adminIds;
            InventoryEntryIds = inventoryEntryIds;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">The other instance with the data being assigned to this</param>
        public LocationData(LocationData other) :
            this(other.Name, other.AddressId, other.OrderIds, other.AdminIds, other.InventoryEntryIds) {
        }
    }
}
