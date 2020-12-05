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
    public class LocationController : Controller
    {
        private readonly Archivist _archivist;

        public LocationController(Archivist archivist) {
            _archivist = archivist;
        }

        // GET: Location/{id}/Orders
        [HttpGet]
        [Authorize(Roles = Roles.AdminAndUser)]
        public async Task<IActionResult> Orders(Guid id) {
            var location = await _archivist.RetrieveAsync<Location>(id);
            string locationName = location.GetName();

            var orders = await _archivist.RetrieveAllAsync<Order>();

            var locationOrders = orders
                .Where(o => {
                    var data = o.GetData() as OrderData;
                    return data.LocationId == id;
                })
                .ToList();

            var viewModels = locationOrders
                .ConvertAll(async co => {
                    // get the data
                    var data = co.GetData() as OrderData;

                    // get the admin name
                    var admin = await _archivist.RetrieveAsync<Admin>(data.AdminId);
                    string adminName = admin.GetName();

                    // get the customer name
                    var customer = await _archivist.RetrieveAsync<Customer>(data.CustomerId);
                    string customerName = customer.GetName();

                    // get the order lines
                    var orderLines = await _archivist.RetrieveSomeAsync<OrderLine>(data.OrderLineIds);

                    // get the orderline view models
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
    }
}
