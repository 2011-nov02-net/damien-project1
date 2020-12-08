using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Domain;
using ArkhenManufacturing.WebApp.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using ArkhenManufacturing.DataAccess;
using Microsoft.AspNetCore.Authorization;
using ArkhenManufacturing.WebApp.Models.Services;

namespace ArkhenManufacturing.WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly Archivist _archivist;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CartService _cartService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(Archivist archivist, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, [FromServices] CartService cartService, ILogger<AccountController> logger) {
            _archivist = archivist;
            _userManager = userManager;
            _signInManager = signInManager;
            _cartService = cartService;
            _logger = logger;
        }

        private IActionResult GetRedirect(string returnUrl) {
            if(Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            } else {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel) {
            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            try {
                var user = new ApplicationUser
                {
                    UserName = viewModel.Username
                };

                var createResult = await _userManager.CreateAsync(user, viewModel.Password);
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");

                if (!createResult.Succeeded) {
                    return View(viewModel);
                } else if (!addToRoleResult.Succeeded) {
                    ModelState.AddModelError(string.Empty, "An error occurred; please try again");
                    return View(viewModel);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                // Redirect to the customer creation view
                return RedirectToAction(nameof(CustomerController.Create), "Customer");
            } catch(Exception ex) {
                _logger.LogError(ex.Message);
                return View(viewModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login() {
            // Log out any users that are currently logged in
            await _signInManager.SignOutAsync();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel) {
            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            try {
                // Try to log in the user here by checking that the data matches
                var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded) {
                    var user = _userManager.GetUserAsync(HttpContext.User);

                    //return GetRedirect(returnUrl);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                } else {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt; please try again");
                    return View(viewModel);
                }
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return View(viewModel);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details() {
            _logger.LogInformation("Details method in AccountController reached.");
            if(!_signInManager.IsSignedIn(HttpContext.User)) {
                return RedirectToAction(nameof(Login), "Account");
            }

            // get the current user id
            var user = await _userManager.GetUserAsync(HttpContext.User);

            // check if the user is an admin or customer
            var roles = await _userManager.GetRolesAsync(user);

            string controllerName;

            if(roles.Contains("Admin")) {
                // is an admin
                controllerName = "Admin";
            } else {
                // is a customer
                controllerName = "Customer";
            }

            return RedirectToAction(nameof(CustomerController.Details), controllerName, user);
        }

        // GET: Account/Cart/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Cart() {
            if (!_signInManager.IsSignedIn(HttpContext.User)) {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            return View(_cartService.ProductRequests);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UpdateCart(string data) {
            if (ModelState.IsValid) {
                var viewModels = JsonSerializer.Deserialize<List<ProductRequestViewModel>>(data);
                _cartService.Clear();
                _cartService.AddRange(viewModels);
            }
            
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(ProductRequestViewModel viewModel) {
            // remove
            _cartService.Remove(viewModel);

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder() {
            // get an admin id for this location by the 
            //      admin with the fewest number of orders
            var admins = await _archivist.RetrieveAllAsync<Admin>();
            var orders = await _archivist.RetrieveAllAsync<Order>();
            var orderData = orders.ConvertAll(o => o.GetData() as OrderData);
            // Get the admin who has the lowest count
            var admin = admins.OrderBy(a => {
                return orderData.Count(od => od.AdminId == a.Id);
            }).First();

            var productIds = _cartService.ProductRequests
                .Select(pr => pr.ProductId)
                .ToList();

            var uniqueLocations = _cartService.ProductRequests
                .Select(pr => pr.LocationId)
                .Distinct()
                .ToList();

            foreach(var itemId in uniqueLocations) {
                // get the ies assocaited with this location

                var inventoryEntries = (await _archivist.RetrieveAllAsync<InventoryEntry>())
                    .Where(ie => (ie.GetData() as InventoryEntryData).LocationId == itemId)
                    .Where(ie => productIds.Contains((ie.GetData() as InventoryEntryData).ProductId))
                    .ToList();

                var inventoryEntriesData = inventoryEntries
                    .Select(ie => ie.GetData() as InventoryEntryData)
                    .ToList();

                var validProducts = _cartService.ProductRequests
                    .Where(pr => inventoryEntriesData.Select(ie => ie.ProductId).Contains(pr.ProductId))
                    .ToList();

                // get the customer id
                var user = await _userManager.GetUserAsync(HttpContext.User);
                Guid customerId = user.UserId;

                // Create the initial order data, and get its id
                var data = new OrderData
                {
                    AdminId = admin.Id,
                    CustomerId = customerId,
                    LocationId = itemId,
                    OrderLineIds = new List<Guid>(),
                    PlacementDate = DateTime.Now
                };

                Guid orderId = await _archivist.CreateAsync<Order>(data);

                // create the order lines
                foreach (var productRequest in validProducts) {
                    var inventoryEntry = inventoryEntries
                        .Select(ie => ie.GetData() as InventoryEntryData)
                        .First(data => data.ProductId == productRequest.ProductId);
                    var product = await _archivist.RetrieveAsync<Product>(productRequest.ProductId);

                    var orderLineData = new OrderLineData(orderId, productRequest.ProductId, productRequest.Count, inventoryEntry.Price, inventoryEntry.Discount);
                    Guid orderLineId = await _archivist.CreateAsync<OrderLine>(orderLineData);
                    data.OrderLineIds.Add(orderLineId);
                }

                // Now to update the backend with the request
                await _archivist.UpdateAsync<Order>(orderId, data);

                // update the store's inventory
                for (int i = 0; i < inventoryEntries.Count; i++) {
                    var inventoryEntry = inventoryEntries[i];
                    var inventoryEntryData = inventoryEntry.GetData() as InventoryEntryData;
                    var productRequest = _cartService.ProductRequests.First(pr => pr.ProductId == inventoryEntryData.ProductId);
                    inventoryEntryData.Count -= productRequest.Count;
                    await _archivist.UpdateAsync<InventoryEntry>(inventoryEntry.Id, inventoryEntryData);
                }

            }

            _cartService.Clear();

            return RedirectToAction(nameof(CustomerController.Orders), "Customer");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
