using System;
using System.Threading.Tasks;

using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.WebApp.Misc;
using ArkhenManufacturing.WebApp.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArkhenManufacturing.WebApp.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly Archivist _archivist;
        private readonly ILogger<AdminController> _logger;

        public AdminController(Archivist archivist, ILogger<AdminController> logger) {
            _archivist = archivist;
            _logger = logger;
        }

        // GET: Admin
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Index() {
            return View();
        }

        // GET: Admin/Details/5
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
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
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Create() {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: Admin/Edit/5
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Edit(Guid id) {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Edit(Guid id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: Admin/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Delete(Guid id) {
            return View();
        }

        // POST: Admin/Delete/5
        [Authorize(Roles = Roles.Admin)]
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
