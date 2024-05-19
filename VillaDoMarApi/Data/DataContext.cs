﻿using Microsoft.EntityFrameworkCore;
using VillaDoMarApi.Entities;

namespace VillaDoMarApi.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public DbSet<TypeProduct> TypeProduct { get; set; }
        public DbSet<Financial> Financials { get; set; }
        public DbSet<FinancialStatus> FinancialStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
