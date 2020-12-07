using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Misc;
using ArkhenManufacturing.WebApp.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArkhenManufacturing.WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly Archivist _archivist;

        public OrderController(Archivist archivist) {
            _archivist = archivist;
        }

        public async Task<IActionResult> Index() {
            var orders = await _archivist.RetrieveAllAsync<Order>();

            var orderSummaries = orders
                .Select(o => (o.Id, o.GetData() as OrderData))
                .Select(async tuple => {
                    Guid orderId = tuple.Item1;
                    var data = tuple.Item2;
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
                        OrderId = orderId,
                        CustomerLink = customerLink,
                        AdminLink = adminLink,
                        LocationLink = locationLink,
                        Total = total,
                        PlacementDate = data.PlacementDate
                    };
                });

            return View(orderSummaries);
        }

        // GET: Order/Details/{id}
        [Authorize(Roles = Roles.AdminAndUser)]
        public async Task<ActionResult> Details(Guid id) {
            var order = await _archivist.RetrieveAsync<Order>(id);
            var data = order.GetData() as OrderData;

            // get the customer's name
            var customer = await _archivist.RetrieveAsync<Customer>(data.CustomerId);
            string customerName = customer.GetName();

            // get the admin's name
            var admin = await _archivist.RetrieveAsync<Admin>(data.AdminId);
            string adminName = admin.GetName();

            // get the location's name
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

            var viewModel = new OrderViewModel(id, customerName, adminName, locationName, orderLineViewModels);

            return View(viewModel);
        }
    }
}
