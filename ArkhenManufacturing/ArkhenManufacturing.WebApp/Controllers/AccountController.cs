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
using Microsoft.AspNetCore.Authentication;
using ArkhenManufacturing.DataAccess;
using Microsoft.AspNetCore.Authorization;
using ArkhenManufacturing.Library.Extensions;
using ArkhenManufacturing.WebApp.Misc;

namespace ArkhenManufacturing.WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly Archivist _archivist;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(Archivist archivist, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger) {
            _archivist = archivist;
            _userManager = userManager;
            _signInManager = signInManager;
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
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl = null) {
            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            try {
                // Try to log in the user here by checking that the data matches
                var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded) {
                    return GetRedirect(returnUrl);
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
        [Authorize(Roles = Roles.AdminAndUser)]
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

            return RedirectToAction(nameof(CustomerController.Details), controllerName, new { id = user.UserId });
        }

        // GET: Account/Cart/
        [AllowAnonymous]
        public IActionResult Cart() {
            if (!_signInManager.IsSignedIn(HttpContext.User)) {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            string cartString = TempData["Cart"]?.ToString();
            List<ProductRequestViewModel> items;

            if (cartString.IsNullOrEmpty()) {
                items = new List<ProductRequestViewModel>();
            } else {
                items = JsonSerializer.Deserialize<List<ProductRequestViewModel>>(cartString);
            }

            return View(items);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(ProductRequestViewModel viewModel) {
            // get the targeted item
            var cart = TempData["Cart"] as List<ProductRequestViewModel>;

            // remove
            cart.Remove(viewModel);

            // return it to the view
            TempData["Cart"] = JsonSerializer.Serialize(cart);
            TempData.Keep("Cart");

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(IEnumerable<ProductRequestViewModel> viewModels) {
            // get an admin id for this location by the 
            //      admin with the fewest number of orders
            var admins = await _archivist.RetrieveAllAsync<Admin>();
            var orders = await _archivist.RetrieveAllAsync<Order>();
            var orderData = orders.ConvertAll(o => o.GetData() as OrderData);
            // Get the admin who has the lowest count
            var admin = admins.OrderBy(a => {
                return orderData.Count(od => od.AdminId == a.Id);
            }).First();
            
            // get the customer id
            Guid customerId = Guid.NewGuid();

            // Create the initial order data, and get its id
            var data = new OrderData
            {
                AdminId = admin.Id,
                CustomerId = customerId,
                LocationId = (Guid)TempData["SelectedLocation"],
                OrderLineIds = new List<Guid>(),
                PlacementDate = DateTime.Now
            };

            Guid orderId = await _archivist.CreateAsync<Order>(data);

            // create the order lines
            foreach(var productRequest in viewModels) {
                var orderLineData = new OrderLineData(orderId, productRequest.ProductId, productRequest.Count, productRequest.PricePerUnit, productRequest.Discount);
                Guid orderLineId = await _archivist.CreateAsync<OrderLine>(orderLineData);
                data.OrderLineIds.Add(orderLineId);
            }

            // Now to update the backend with the request
            await _archivist.UpdateAsync<Order>(orderId, data);

            TempData["Message"] = "Order placed successfully";
            TempData["Cart"] = JsonSerializer.Serialize(new List<ProductRequestViewModel>());
            return RedirectToAction(nameof(OrderController.Details), "Order", new { id = orderId });
        }

        [Authorize]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
