using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Extensions;
using ArkhenManufacturing.WebApp.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArkhenManufacturing.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Archivist _archivist;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(Archivist archivist, ILogger<CustomerController> logger) {
            _archivist = archivist;
            _logger = logger;
        }

        // GET: Customer
        public ActionResult Index() {
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(Guid id) {
            try {
                // Get the requested data
                var customer = _archivist.Retrieve<Customer>(id);
                customer.NullCheck(nameof(customer));
                var customerData = customer.GetData() as CustomerData;
                var address = _archivist.Retrieve<Address>(customerData.AddressId);

                // Create the ViewModel and show the data
                var viewModel = new CustomerViewModel(customer, address);
                return View(viewModel);
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        // GET: Customer/{id}/Orders
        public async Task<IActionResult> Orders(Guid id) {
            var customer = await _archivist.RetrieveAsync<Customer>(id);
            var customerName = customer.GetName();

            var orders = await _archivist.RetrieveAllAsync<Order>();

            var customerOrders = orders
                .Where(o => {
                    var data = o.GetData() as OrderData;
                    return data.CustomerId == id;
                })
                .ToList();

            var viewModels = customerOrders
                .ConvertAll(async co => {
                    // get the data
                    var data = co.GetData() as OrderData;

                    var admin = await _archivist.RetrieveAsync<Admin>(data.AdminId);
                    string adminName = admin.GetName();

                    // get the location name
                    var location = await _archivist.RetrieveAsync<Location>(data.LocationId);
                    string locationName = location.GetName();

                    // get the order lines
                    var orderLines = await _archivist.RetrieveSomeAsync<OrderLine>(data.OrderLineIds);

                    var orderLineViewModels = orderLines
                        .Select(async ol => {
                            var olData = ol.GetData() as OrderLineData;
                            var product = await _archivist.RetrieveAsync<Product>(olData.ProductId);
                            string productName = product.GetName();
                            return new OrderLineViewModel(productName, olData);
                        })
                        .Select(t => t.Result)
                        .ToList();

                    return new OrderViewModel(customerName, adminName, locationName, orderLineViewModels);
                });

            return View(viewModels);
        }

        // GET: Customer/SearchByName/{name}
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
        public ActionResult Create() {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel viewModel) {
            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            string json = TempData["RegistrationData"].ToString();
            var userData = JsonSerializer.Deserialize<RegisterViewModel>(json);

            try {
                var data = (CustomerData)viewModel;
                data.Username = userData.Username;
                data.Password = userData.Password;

                _archivist.Create<Customer>(data);
                TempData["Message"] = $"'{data.LastName}, {data.FirstName}' created successfully.";
                return Redirect("/Account/Login");
            } catch {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(Guid id) {
            try {
                var customer = _archivist.Retrieve<Customer>(id);
                var customerData = customer.GetData() as CustomerData;
                var address = _archivist.Retrieve<Address>(customerData.AddressId);
                var viewModel = new CustomerViewModel(customer, address);
                return View(viewModel);
            } catch {

            }

            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, CustomerViewModel viewModel) {
            try {
                if (!ModelState.IsValid) {
                    throw new Exception("Invalid ModelState");
                }

                return RedirectToAction(nameof(Index));
            } catch {
                // Return the page to the editing page
                return View(viewModel);
            }
        }

        // GET: Customer/Delete/5
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
        public ActionResult Delete(Guid id, IFormCollection collection) {
            try {

                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}
