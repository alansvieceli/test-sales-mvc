using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testSalesMVC.Models;

namespace testSalesMVC.Services {

    public class DepartmentService {

        private readonly testSalesMVCContext _context;

        public DepartmentService(testSalesMVCContext context) {
            _context = context;
        }

        public List<Department> FindAll() {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

        public Department FindById(int? id) {
            return (id == null) ? null : _context.Department.FirstOrDefault(m => m.Id == id);
        }

        public void Add(Department department) {
            _context.Add(department);
            _context.SaveChanges();
        }

        public void Delete(int? id) {
            var department = _context.Department.Find(id);
            _context.Department.Remove(department);
            _context.SaveChanges();
        }

        public Department Edit(Department department) {
            try {
                _context.Update(department);
                _context.SaveChanges();
                return department;
            }
            catch (DbUpdateConcurrencyException) {
                if (!Exists(department.Id)) {
                    return null;
                } else {
                    throw;
                }
            }
        }

        private bool Exists(int id) {
            return _context.Department.Any(e => e.Id == id);
        }
    }

}
