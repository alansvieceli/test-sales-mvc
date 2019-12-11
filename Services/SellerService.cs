using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testSalesMVC.Models;

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
            return (id == null) ? null : _context.Seller.FirstOrDefault(m => m.Id == id);
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

        public Seller Update(Seller seller) {
            try {
                _context.Update(seller);
                _context.SaveChanges();
                return seller;
            }
            catch (DbUpdateConcurrencyException) {
                if (!Exists(seller.Id)) {
                    return null;
                } else {
                    throw;
                }
            }
        }

        private bool Exists(int id) {
            return _context.Seller.Any(e => e.Id == id);
        }
    }

}
