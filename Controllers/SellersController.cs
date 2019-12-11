using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testSalesMVC.Models;
using testSalesMVC.Models.ViewModels;
using testSalesMVC.Services;

namespace testSalesMVC.Controllers {
    public class SellersController : Controller {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: Sellers
        public IActionResult Index() {
            return View( _sellerService.FindAll());
        }

        // GET: Sellers/Details/5
        public IActionResult Details(int? id) {

            var seller =  _sellerService.FindById(id.Value);

            if (seller == null) {
                return NotFound();
            }

            return View(seller);

        }

        // GET: Sellers/Create
        public IActionResult Create() {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) {

            if (ModelState.IsValid) {
                _sellerService.Insert(seller);
                return RedirectToAction(nameof(Index));
            }
            return View(seller);

        }

        // GET: Sellers/Edit/5
        public IActionResult Edit(int id) {

            var seller =  _sellerService.FindById(id);

            if (seller == null) {
                return NotFound();
            }

            return View(seller);

        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Email,BirthDate,baseSalary")] Seller seller) {

            if (id != seller.Id) {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var sellerRet = _sellerService.Update(seller);
                if (sellerRet == null) {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);

        }

        // GET: Sellers/Delete/5
        public IActionResult Delete(int? id) {

            var seller = _sellerService.FindById(id.Value);

            if (seller == null) {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id) {
            _sellerService.Delete(id.Value);
            return RedirectToAction(nameof(Index));
        }

    }
}
