using System;
using System.Threading.Tasks;

using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArkhenManufacturing.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly Archivist _archivist;
        private readonly ILogger<AdminController> _logger;

        public AdminController(Archivist archivist, ILogger<AdminController> logger) {
            _archivist = archivist;
            _logger = logger;
        }

        // GET: Admin
        public IActionResult Index() {
            return View();
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(Guid id) {
            try {
                var admin = await _archivist.RetrieveAsync<Admin>(id);
                var viewModel = new AdminViewModel(admin.GetData() as AdminData);
                return View(viewModel);
            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return RedirectToRoute("Home", "Index");
            }
        }

        // GET: Admin/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(Guid id) {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(Guid id) {
            return View();
        }

        // POST: Admin/Delete/5
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
