using System;
using System.ComponentModel.DataAnnotations;

namespace ArkhenManufacturing.WebApp.Models
{
    public class ProductRequestViewModel
    {
        public Guid ProductId { get; set; }
        [Range(1, 999999)]
        public string ProductName { get; set; }
        public int Count { get; set; }

        public ProductRequestViewModel() { }

        public ProductRequestViewModel(Guid productId) {
            ProductId = productId;
        }
    }
}
