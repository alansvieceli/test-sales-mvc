﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace testSalesMVC.Models
{
    public class testSalesMVCContext : DbContext
    {
        public testSalesMVCContext (DbContextOptions<testSalesMVCContext> options) : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }        
    }
}
