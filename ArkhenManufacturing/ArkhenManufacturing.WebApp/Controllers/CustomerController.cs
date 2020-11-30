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
            } catch(Exception ex) {
                _logger.LogError(ex.Message);
                return View();
            }
        }

        // GET: Customer/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel viewModel) {
            if(!ModelState.IsValid) {
                return View(viewModel);
            }

            string json = TempData["RegistrationData"].ToString();
            var userData = JsonSerializer.Deserialize<RegisterViewModel>(json);

            try {
                var data = (CustomerData)viewModel;
                data.Username = userData.Username;
                data.Password = userData.Password;

                _archivist.Create<Customer>(data);
                TempData["SuccessMessage"] = $"'{data.LastName}, {data.FirstName}' created successfully.";
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
                if(!ModelState.IsValid) {
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
