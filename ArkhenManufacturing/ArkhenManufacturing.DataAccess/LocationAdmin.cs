using System;

namespace ArkhenManufacturing.DataAccess
{
    public class LocationAdmin
    {
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public Guid AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}
