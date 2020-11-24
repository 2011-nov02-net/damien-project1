using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArkhenManufacturing.DataAccess
{
    public class InventoryEntry
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(3,3)")]
        public decimal Discount { get; set; }
    }
}
