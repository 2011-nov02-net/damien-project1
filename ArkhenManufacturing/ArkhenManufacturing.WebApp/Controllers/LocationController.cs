using System;
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

        // GET: Location/[Index]
        public async Task<IActionResult> Index() {
            var locations = await _archivist.RetrieveAllAsync<Location>();

            var viewModels = locations
                .Select(l => new Tuple<Guid, LocationData>(l.Id, l.GetData() as LocationData))
                .Select(data => new LocationViewModel { Name = data.Item2.Name, Id = data.Item1 });

            return View(viewModels);
        }

        public async Task<IActionResult> Admins(Guid id) {
            var location = await _archivist.RetrieveAsync<Location>(id);
            var locationData = location.GetData() as LocationData;

            var adminIdsAndViewModels = (await _archivist.RetrieveSomeAsync<Admin>(locationData.AdminIds))
                .Select(a => {
                    var data = a.GetData() as AdminData;
                    return new AdminViewModel(data);
                })
                .ToList();

            return View(adminIdsAndViewModels);
        }

        // GET: Location/{id}/Orders
        [HttpGet]
        [Authorize(Roles = Roles.AdminAndUser)]
        public async Task<IActionResult> Orders(Guid id) {
            var location = await _archivist.RetrieveAsync<Location>(id);
            string locationName = location.GetName();

            var orders = (await _archivist.RetrieveAllAsync<Order>())
                .Where(o => {
                    var data = o.GetData() as OrderData;
                    return data.LocationId == id;
                })
                .ToList();

            var orderSummaries = orders
                .Select(o => new Tuple<Guid, OrderData>(o.Id, o.GetData() as OrderData))
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

                    var orderLines = (await _archivist.RetrieveAllAsync<OrderLine>())
                        .Where(ol => (ol.GetData() as OrderLineData).OrderId == orderId);

                    var orderLineIds = orderLines
                        .Select(ol => ol.Id);

                    // Get the total price
                    decimal total = orderLineIds
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
                })
                .Select(t => t.Result);

            return View(orderSummaries);
        }
    }
}
