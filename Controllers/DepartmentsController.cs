using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testSalesMVC.Models;
using testSalesMVC.Models.ViewModels;
using testSalesMVC.Services;
using testSalesMVC.Services.Exceptions;

namespace testSalesMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentsController(DepartmentService service)
        {
            _departmentService = service;
        }

        // GET: Departments
        public async Task<IActionResult> Index() {
            return View(await _departmentService.FindAllAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int id) {

            var department = await _departmentService.FindByIdAsync(id);
            if (department == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(department);

        }

        // GET: Departments/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department)
        {
            if (ModelState.IsValid) {
                await _departmentService.InsertAsync(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var department = await _departmentService.FindByIdAsync(id);
            if (department == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Department department)
        {
            if (id != department.Id) {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            if (ModelState.IsValid) {
                try {
                    await _departmentService.UpdateAsync(department);                    
                }
                catch (ApplicationException e) {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var department = await _departmentService.FindByIdAsync(id);
            if (department == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try { 
                await _departmentService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            } 
            catch(IntegrityException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
