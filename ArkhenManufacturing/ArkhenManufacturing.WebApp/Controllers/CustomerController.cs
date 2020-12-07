using System;
using System.Linq;
using System.Threading.Tasks;

using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Extensions;
using ArkhenManufacturing.WebApp.Misc;
using ArkhenManufacturing.WebApp.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArkhenManufacturing.WebApp.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly Archivist _archivist;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(Archivist archivist, UserManager<ApplicationUser> userManager, ILogger<CustomerController> logger) {
            _archivist = archivist;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Customer
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Index(string searchString) {
            var customers = await _archivist.RetrieveByNameAsync<Customer>(searchString);

            var viewModels = customers
                .ConvertAll(async c => {
                    var data = c.GetData() as CustomerData;
                    var address = await _archivist.RetrieveAsync<Address>(data.AddressId);
                    return new CustomerViewModel(c, address);
                });

            return View(viewModels);
        }

        // GET: Customer/Details/5
        [HttpGet]
        [Authorize(Roles = Roles.AdminAndUser)]
        public async Task<ActionResult> Details(ApplicationUser user) {
            // Get the requested data
            var customer = await _archivist.RetrieveAsync<Customer>(user.UserId);
            customer.NullCheck(nameof(customer));
            var customerData = customer.GetData() as CustomerData;
            var address = await _archivist.RetrieveAsync<Address>(customerData.AddressId);

            // Create the ViewModel and show the data
            var viewModel = new CustomerViewModel(customer, address);
            return View(viewModel);
        }

        // GET: Customer/{id}/Orders
        [HttpGet]
        [Authorize(Roles = Roles.AdminAndUser)]
        public async Task<IActionResult> Orders(Guid id) {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var customer = await _archivist.RetrieveAsync<Customer>(user.UserId);
            var customerName = customer.GetName();

            var orders = await _archivist.RetrieveAllAsync<Order>();

            orders = orders
                .Where(o => {
                    var data = o.GetData() as OrderData;
                    return data.CustomerId == id;
                })
                .ToList();

            var orderSummaries = orders
                .Select(o => o.GetData() as OrderData)
                .Select(async data => {
                    // get the customer's name
                    var customer = await _archivist.RetrieveAsync<Customer>(data.CustomerId);
                    string customerName = customer.GetName();

                    // get the admin's name
                    var admin = await _archivist.RetrieveAsync<Admin>(data.AdminId);
                    string adminName = admin.GetName();

                    // get the location's name
                    var location = await _archivist.RetrieveAsync<Location>(data.LocationId);
                    string locationName = location.GetName();

                    // Get the total price
                    decimal total = data.OrderLineIds
                        .Select(async id => await _archivist.RetrieveAsync<OrderLine>(id))
                            .Select(t => t.Result)
                        .Select(ol => ol.GetData() as OrderLineData)
                        .Sum(olData => olData.TotalPrice);

                    Tuple<string, Guid> customerLink = new(customerName, data.CustomerId);

                    Tuple<string, Guid> adminLink = new(adminName, data.AdminId);

                    Tuple<string, Guid> locationLink = new(locationName, data.LocationId);

                    return new OrderSummaryViewModel
                    {
                        CustomerLink = customerLink,
                        AdminLink = adminLink,
                        LocationLink = locationLink,
                        Total = total,
                        PlacementDate = data.PlacementDate
                    };
                })
                .Select(t => t.Result);

            return View(orderSummaries);
        }

        // GET: Customer/SearchByName/{name}
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> SearchByName(string id) {
            var customers = await _archivist.RetrieveByNameAsync<Customer>(id);

            var viewModels = customers
                .ConvertAll(async c => {
                    var data = c.GetData() as CustomerData;
                    var address = await _archivist.RetrieveAsync<Address>(data.AddressId);
                    return await Task.Run(() => new CustomerViewModel(c, address));
                })
                .Select(t => t.Result)
                .ToList();

            return View(viewModels);
        }

        // GET: Customer/Create
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create(Guid userId) {
            ViewData["UserId"] = userId;
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerViewModel viewModel) {
            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var customerData = (CustomerData)viewModel;
            var addressData = (AddressData)viewModel;

            Guid customerId = _archivist.Create<Customer>(customerData);
            _ = _archivist.Create<Address>(addressData);

            // Update the user's id to match the customer's id
            currentUser.UserId = customerId;
            currentUser.Email = viewModel.Email;

            TempData["Message"] = $"'{customerData.LastName}, {customerData.FirstName}' created successfully.";

            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        // GET: Customer/Edit/5
        [HttpGet]
        [Authorize(Roles = Roles.AdminAndUser)]
        public ActionResult Edit(Guid id) {
            var customer = _archivist.Retrieve<Customer>(id);
            var customerData = customer.GetData() as CustomerData;
            var address = _archivist.Retrieve<Address>(customerData.AddressId);
            var viewModel = new CustomerViewModel(customer, address);
            return View(viewModel);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.AdminAndUser)]
        public async Task<ActionResult> Edit(Guid id, CustomerViewModel viewModel) {
            try {
                if (!ModelState.IsValid) {
                    throw new Exception("Invalid ModelState");
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);

                var customerData = (CustomerData)viewModel;
                var addressData = (AddressData)viewModel;
                await _archivist.UpdateAsync<Customer>(user.UserId, customerData);
                await _archivist.UpdateAsync<Address>(customerData.AddressId, addressData);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            } catch {
                // Return the page to the editing page
                return View(viewModel);
            }
        }

        // GET: Customer/Delete/5
        [HttpGet]
        [Authorize(Roles = Roles.AdminAndUser)]
        public ActionResult Delete(Guid id) {
            try {
                _archivist.Delete<Customer>(id);
            } catch {

            }
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.AdminAndUser)]
        public ActionResult Delete(Guid id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}
