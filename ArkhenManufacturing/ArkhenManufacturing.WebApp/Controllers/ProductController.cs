using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArkhenManufacturing.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly Archivist _archivist;
        private readonly ILogger<ProductController> _logger;

        public ProductController(Archivist archivist, ILogger<ProductController> logger) {
            _archivist = archivist;
            _logger = logger;
        }

        // GET: Product
        public ActionResult Index() {
            var products = _archivist.RetrieveAll<Product>();

            Dictionary<string, ProductViewModel> productViewModels = new Dictionary<string, ProductViewModel>();
            var inventoryEntries = _archivist.RetrieveAll<InventoryEntry>();

            foreach(var ie in inventoryEntries) {
                var ieData = ie.GetData() as InventoryEntryData;
                var firstProduct = products.First(p => p.Id == ieData.ProductId);
                string productName = firstProduct.GetName();
                
                if(!productViewModels.ContainsKey(productName)) {
                    productViewModels[productName] = new ProductViewModel(productName, ieData);
                }
            }

            return View("Products", productViewModels.Values);
        }

        // GET: Product/Details/5
        public ActionResult Details(Guid id) {
            var product = _archivist.Retrieve<Product>(id);
            Guid? defaultStoreId = TempData.Peek("DefaultStoreId") as Guid?;
            InventoryEntryData inventoryEntry = _archivist
                .RetrieveAll<InventoryEntry>()
                .Select(ie => ie.GetData() as InventoryEntryData)
                .FirstOrDefault(data => data.ProductId == id);

            if (defaultStoreId.HasValue) {
                if (inventoryEntry?.LocationId != defaultStoreId.Value) {
                    inventoryEntry = null;
                }
            }

            if (inventoryEntry is null) {
                // the product doesn't exist
                ModelState.AddModelError("", "Product is not found at any store.");
            } else if (inventoryEntry.Count == 0) {
                // product is out of stock
                ModelState.AddModelError("", "Product is out of stock.");
            } else {
                // product is in stock
                var productData = product.GetData() as ProductData;
                var viewModel = new ProductViewModel(productData.Name, inventoryEntry);
                return View(viewModel);
            }

            return View(id);
        }

        // POST: Product/AddToCart/{id}
        public async Task<IActionResult> AddToCart(ProductRequestViewModel viewModel) {
            if(!ModelState.IsValid) {
                return View(viewModel);
            }

            if (TempData["Cart"] is not List<ProductRequestViewModel> productsInCart) {
                // No items are in cart, the user must select a location
                productsInCart = new List<ProductRequestViewModel>();
            }

            productsInCart.Add(viewModel);
            TempData["Cart"] = JsonSerializer.Serialize(productsInCart);
            TempData.Keep("Cart");

            if(TempData["SelectedLocation"] is null) {
                // redirect the user to another page to select a location
                return RedirectToAction("Retrieve", viewModel);
            }

            // Send the user to a page that directs the user to checkout or the homepage
            TempData["SuccessMessage"] = "Item added successfully";
            return await Task.Run(() => Redirect("/Home/Index"));
        }

        public async Task<IActionResult> Retrieve(ProductRequestViewModel viewModel) {
            var id = viewModel.ProductId;
            var inventoryEntries = await _archivist.RetrieveAllAsync<InventoryEntry>();

            var storeIds = inventoryEntries
                .Select(ie => ie.GetData() as InventoryEntryData)
                .Where(data => data.ProductId == id)
                .Select(data => data.LocationId)
                .ToList();

            var locations = await _archivist.RetrieveSomeAsync<Location>(storeIds);

            // get the locations that offer the product
            var locationNames = locations
                .Select(l => new Tuple<string, Guid>(l.GetName(), l.Id))
                .ToList();

            var locationProductRequestViewModel = new LocationProductRequestViewModel
            {
                LocationNamesWithIds = locationNames,
                ProductRequestViewModel = viewModel
            };

            return View(locationProductRequestViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Retrieve(LocationProductRequestViewModel viewModel) {
            if(!ModelState.IsValid) {
                return View(viewModel.ProductRequestViewModel);
            }

            TempData["SelectedLocation"] = viewModel.SelectedLocationId;

            return RedirectToAction("AddToCart", viewModel.ProductRequestViewModel);
        }

        // GET: Product/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel) {
            // TODO: Fill out create method for a product
            throw new NotImplementedException();

            // if (!ModelState.IsValid) {
            //     return View(viewModel);
            // }
            // 
            // try {
            //                     return RedirectToAction(nameof(Index));
            // } catch {
            //     return View();
            // }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(Guid id) {

            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(Guid id) {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}
