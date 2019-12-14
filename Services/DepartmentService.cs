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

        public async Task<List<Department>> FindAllAsync() {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Department> FindByIdAsync(int? id) {
            return (id == null) ? null : await _context.Department.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(Department department) {
            _context.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id) {
            var department = await _context.Department.FindAsync(id);

            try {
                _context.Department.Remove(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Department department) {

            var hasAny = await _context.Department.AnyAsync(s => s.Id == department.Id);
            if (!hasAny) {
                throw new NotFoundException("Id not found");
            }

            try {
                _context.Update(department);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }

}
