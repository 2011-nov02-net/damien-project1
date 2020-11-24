using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArkhenManufacturing.DataAccess
{
    public class DbOrderLine
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DbOrder Order { get; set; }
        public Guid ProductId { get; set; }
        public DbProduct Product { get; set; }
        public int Count { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerUnit { get; set; }
        [Column(TypeName = "decimal(3,3)")]
        public decimal Discount { get; set; }
    }
}
