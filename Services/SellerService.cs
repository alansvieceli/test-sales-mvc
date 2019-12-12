using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testSalesMVC.Models;
using Microsoft.EntityFrameworkCore;
using testSalesMVC.Services.Exceptions;

namespace testSalesMVC.Services {

    public class SellerService {

        private readonly testSalesMVCContext _context;

        public SellerService(testSalesMVCContext context) {
            _context = context;
        }

        public List<Seller> FindAll() {
            return _context.Seller.ToList();
        }

        public Seller FindById(int? id) {
            return (id == null) ? null : _context.Seller.Include(s => s.Department).FirstOrDefault(m => m.Id == id);
        }

        public void Insert(Seller seller) {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public void Delete(int? id) {
            var seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller seller) {

            if (!_context.Seller.Any(s => s.Id == seller.Id)) {
                throw new NotFoundException("Id not found");
            }

            try {
                _context.Update(seller);
                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }

        }
    }

}
