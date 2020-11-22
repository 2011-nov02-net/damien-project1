using System;

namespace ArkhenManufacturing.DataAccess
{
    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Discount { get; set; }
    }
}
