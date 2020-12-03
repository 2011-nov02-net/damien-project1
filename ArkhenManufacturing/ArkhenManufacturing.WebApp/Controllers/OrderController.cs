using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Models;

using Microsoft.AspNetCore.Mvc;

namespace ArkhenManufacturing.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly Archivist _archivist;

        public OrderController(Archivist archivist) {
            _archivist = archivist;
        }

        // GET: Order/Details/{id}
        public async Task<ActionResult> Details(Guid id) {
            var order = await _archivist.RetrieveAsync<Order>(id);
            var data = order.GetData() as OrderData;

            var customer = await _archivist.RetrieveAsync<Customer>(data.CustomerId);
            string customerName = customer.GetName();

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

            var viewModel = new OrderViewModel(customerName, adminName, locationName, orderLineViewModels);

            return View(viewModel);
        }
    }
}
