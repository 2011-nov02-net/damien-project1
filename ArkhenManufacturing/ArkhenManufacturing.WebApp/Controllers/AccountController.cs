using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Domain;
using ArkhenManufacturing.WebApp.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using ArkhenManufacturing.WebApp.Misc;

namespace ArkhenManufacturing.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly Archivist _archivist;
        private readonly ILogger<AccountController> _logger;
        private readonly IEncrypter _encrypter;

        public AccountController(Archivist archivist, ILogger<AccountController> logger, IEncrypter encrypter) {
            _archivist = archivist;
            _logger = logger;
            _encrypter = encrypter;
        }

        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel) {
            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            try {
                var customers = await Task.Run(() => _archivist.RetrieveAllAsync<Customer>()
                    .Result
                        .Select(c => c.GetData() as CustomerData)
                        .Where(cd => cd.Username == viewModel.Username));

                if (customers.Any()) {
                    ModelState.AddModelError("Username", "Username is already taken; try again.");
                    throw new ArgumentException($"User with username '{viewModel.Username}' already exists.");
                }

                // Encrypt the password, since a user is being created
                viewModel.Password = _encrypter.Encrypt(viewModel.Password);

                // customer doesn't exist, so this can be done. Redirect them to the customer creation though
                TempData["RegistrationData"] = JsonSerializer.Serialize(viewModel);
                TempData["SuccessMessage"] = $"User '{viewModel.Username}' created successfully.";

                // Redirect to the customer creation view
                return Redirect("/Customer/Create");
            } catch {
                return View(viewModel);
            }
        }

        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel) {
            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            try {
                // Try to log in the user here by checking that the data matches
                var customerData = await Task.Run(() => _archivist
                    .RetrieveAll<Customer>()
                    .Select(c => c.GetData() as CustomerData)
                    .FirstOrDefault(c => c.Username == viewModel.Username));

                if (customerData is null) {
                    TempData["SuccessMessage"] = null;
                    ModelState.AddModelError("", "User does not exist");
                    return View(viewModel);
                } else {
                    string encryptedPassword = _encrypter.Encrypt(viewModel.Password);

                    if (customerData.Password != encryptedPassword) {
                        // login was not successful
                        ModelState.AddModelError("", "Username/Password mismatch");
                        throw new ApplicationException("Username/Password mismatch");
                    }
                }

                // "Login" the user; credentials are correct
                TempData["CurrentUser"] = viewModel.Username;
                TempData.Keep("CurrentUser");

                return Redirect("/Home");
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return View(viewModel);
            }
        }

        // TODO: Get the currently logged in user and show their details
        public IActionResult Details() {
            return View();
        }

        // GET: Account/Cart/
        public IActionResult Cart() {
            if(TempData["CurrentUser"] is null) {
                return RedirectToAction("Login", "Account");
            }

            List<ProductRequestViewModel> items = JsonSerializer.Deserialize<List<ProductRequestViewModel>>(TempData["Cart"]?.ToString());

            if (items is not List<ProductRequestViewModel> productsInCart) {
                productsInCart = new List<ProductRequestViewModel>();
            }

            return View(productsInCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(ProductRequestViewModel viewModel) {
            // get the targeted item
            var cart = TempData["Cart"] as List<ProductRequestViewModel>;
            // remove
            cart.Remove(viewModel);
            // return it to the view
            TempData["Cart"] = JsonSerializer.Serialize(cart);
            TempData.Keep("Cart");
            return Redirect("/Account/Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(IEnumerable<ProductRequestViewModel> viewModels) {
            var productIds = viewModels
                .Select(vm => vm.ProductId)
                .ToList();

            // get an admin id for this location by the 
            //      admin with the fewest number of orders

            // get the customer id
            Guid customerId = Guid.NewGuid();

            // create the order lines

            // Now to update the backend with the request
            var data = new OrderData
            {
                AdminId = Guid.NewGuid(),
                CustomerId = customerId,
                LocationId = (Guid)TempData["SelectedLocation"],
                OrderLineIds = new List<Guid>(),
                PlacementDate = DateTime.Now
            };

            Guid orderId = await _archivist.CreateAsync<Order>(data);

            TempData["SuccessMessage"] = "Order placed successfully";
            TempData["Cart"] = JsonSerializer.Serialize(new List<ProductRequestViewModel>());
            return RedirectToAction("Details", "Order", new { id = orderId });
        }

        public IActionResult Logout() {
            TempData["CurrentUser"] = null;
            return Redirect("/Home");
        }
    }
}
