using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArkhenManufacturing.WebApp.Models
{
    public class ProductRequestViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        [Range(1, 999999)]
        public int Count { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Discount { get; set; }
        public ICollection<Tuple<string, Guid>> PossibleLocations { get; set; }
        public Guid LocationId { get; set; }

        public ProductRequestViewModel() { }

        public ProductRequestViewModel(Guid productId) {
            ProductId = productId;
        }
    }
}
