using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testSalesMVC.Models;
using testSalesMVC.Services.Exceptions;

namespace testSalesMVC.Services {

    public class SellerService {

        private readonly testSalesMVCContext _context;

        public SellerService(testSalesMVCContext context) {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync() {
            return await _context.Seller.ToListAsync();
        }

        public async Task<Seller> FindByIdAsync(int? id) {
            return (id == null) ? null : await _context.Seller.Include(s => s.Department).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(Seller seller) {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id) {
            var seller = await _context.Seller.FindAsync(id);

            try {
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller seller) {

            var hasAny = await _context.Seller.AnyAsync(s => s.Id == seller.Id);
            if (! hasAny) {
                throw new NotFoundException("Id not found");
            }

            try {
                _context.Update(seller);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }

}
