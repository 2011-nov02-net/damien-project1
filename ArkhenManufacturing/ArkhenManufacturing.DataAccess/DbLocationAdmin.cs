using System;

namespace ArkhenManufacturing.DataAccess
{
    public class DbLocationAdmin : DbEntity
    {
        public Guid LocationId { get; set; }
        public DbLocation Location { get; set; }
        public Guid AdminId { get; set; }
        public DbAdmin Admin { get; set; }
    }
}
