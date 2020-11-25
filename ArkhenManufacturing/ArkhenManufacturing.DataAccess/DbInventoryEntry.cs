using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArkhenManufacturing.DataAccess
{
    public class DbInventoryEntry : DbEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public DbProduct Product { get; set; }
        public Guid LocationId { get; set; }
        public DbLocation Location { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(3,3)")]
        public decimal Discount { get; set; }
        public int Count { get; set; }
        public int Threshold { get; set; }
    }
}
