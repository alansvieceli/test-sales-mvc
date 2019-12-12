using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testSalesMVC.Models;
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
        public IActionResult Index() {
            return View(_departmentService.FindAll());
        }

        // GET: Departments/Details/5
        public IActionResult Details(int id) {

            var department = _departmentService.FindById(id);

            if (department == null) {
                return NotFound();
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
        public IActionResult Create([Bind("Id,Name")] Department department)
        {
            if (ModelState.IsValid) {
                _departmentService.Insert(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int? id)
        {
            var department = _departmentService.FindById(id);

            if (department == null) {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Department department)
        {
            if (id != department.Id) {
                return BadRequest();
            }

            if (ModelState.IsValid) {
                try {
                    _departmentService.Update(department);                    
                }
                catch (NotFoundException e) {
                    return NotFound();
                }
                catch (DbConcurrencyException e) {
                    return BadRequest();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(int? id)
        {
            var department = _departmentService.FindById(id);

            if (department == null) {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _departmentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
