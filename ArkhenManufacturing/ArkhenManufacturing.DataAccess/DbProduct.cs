using System;

namespace ArkhenManufacturing.DataAccess
{
    public class DbProduct : DbEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
