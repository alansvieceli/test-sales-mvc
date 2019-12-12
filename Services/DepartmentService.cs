using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testSalesMVC.Models;
using testSalesMVC.Services.Exceptions;

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

        public void Insert(Department department) {
            _context.Add(department);
            _context.SaveChanges();
        }

        public void Delete(int? id) {
            var department = _context.Department.Find(id);
            _context.Department.Remove(department);
            _context.SaveChanges();
        }

        public void Update(Department department) {
            if (!_context.Department.Any(s => s.Id == department.Id)) {
                throw new NotFoundException("Id not found");
            }

            try {
                _context.Update(department);
                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }

}
