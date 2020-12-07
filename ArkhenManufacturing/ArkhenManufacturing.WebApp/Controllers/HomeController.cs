using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArkhenManufacturing.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Archivist _archivist;
        private readonly ILogger<HomeController> _logger;

        public HomeController(Archivist archivist, ILogger<HomeController> logger) {
            _archivist = archivist;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index() {
            var products = _archivist.RetrieveAll<Product>();

            Dictionary<string, ProductViewModel> productViewModels = new Dictionary<string, ProductViewModel>();
            var inventoryEntries = _archivist.RetrieveAll<InventoryEntry>();

            foreach (var ie in inventoryEntries) {
                var ieData = ie.GetData() as InventoryEntryData;
                var firstProduct = products.First(p => p.Id == ieData.ProductId);
                string productName = firstProduct.GetName();

                if (!productViewModels.ContainsKey(productName)) {
                    productViewModels[productName] = new ProductViewModel(productName, ieData);
                }
            }

            return View(productViewModels.Values);
        }

        [AllowAnonymous]
        public IActionResult Privacy() {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
