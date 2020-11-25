using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArkhenManufacturing.WebApp.Controllers
{
    public class AddressController : Controller
    {
        // GET: AddressController
        public ActionResult Index() {
            return View();
        }

        // GET: AddressController/Details/5
        public ActionResult Details(Guid id) {
            var item = ArchivistInterface.Retrieve<Address>(id);
            var viewModel = new AddressViewModel(item);
            return View(viewModel);
        }

        // GET: AddressController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: AddressController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressViewModel viewModel) {
            try {
                var data = (AddressData)viewModel;
                _ = ArchivistInterface.Create<Address>(data);
                TempData["SuccessMessage"] = "Address created successfully.";
                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ArchivistInterface.LogLine(ex.Message);
                return View(viewModel);
            }
        }

        // GET: AddressController/Edit/5
        public ActionResult Edit(Guid id) {
            return View();
        }

        // POST: AddressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, AddressViewModel viewModel) {
            try {
                if(!ModelState.IsValid) {
                    return View(viewModel);
                }

                var data = (AddressData)viewModel;
                _ = ArchivistInterface.Create<Address>(data);
                return RedirectToAction(nameof(Index));
            } catch(Exception ex) {
                ArchivistInterface.LogLine(ex.Message);
                return View(viewModel);
            }
        }

        // GET: AddressController/Delete/5
        public ActionResult Delete(Guid id) {
            var item = ArchivistInterface.Retrieve<Address>(id);
            var viewModel = new AddressViewModel(item);
            return View();
        }

        // POST: AddressController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection) {
            try {
                ArchivistInterface.Delete<Address>(id);
                return RedirectToAction(nameof(Index));
            } catch(Exception ex) {
                ArchivistInterface.LogLine(ex.Message);
                return View(id);
            }
        }
    }
}
