using System.Collections.Generic;

namespace ArkhenManufacturing.WebApp.Models.Services
{
    public class CartService
    {
        private List<ProductRequestViewModel> _products;

        public List<ProductRequestViewModel> ProductRequests { get => _products; }

        public CartService() {
            _products = new List<ProductRequestViewModel>();
        }

        public void Clear() => _products.Clear();
        public void Add(ProductRequestViewModel product) => _products.Add(product);
        public void AddRange(IEnumerable<ProductRequestViewModel> viewModels) => _products.AddRange(viewModels);
        public void Remove(ProductRequestViewModel product) => _products.Remove(product);
    }
}
