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
        public Guid AddressId { get; set; }
        public List<Guid> OrderIds { get; set; }
        public List<Guid> AdminIds { get; set; }
        public List<Guid> InventoryEntries { get; set; }

        public LocationData(string name) :
            base(name) {
            OrderIds = new List<Guid>();
        }

        public LocationData(string name, List<Guid> orderIds, List<Guid> adminIds) :
            base(name) {
            OrderIds = orderIds;
            AdminIds = adminIds;
        }
    }
}
