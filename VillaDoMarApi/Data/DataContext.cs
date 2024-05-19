using Microsoft.EntityFrameworkCore;
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
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; } 
        public DbSet<Financial> Financials { get; set; }
        public DbSet<FinancialStatus> FinancialStatus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RawProduct> RawProducts { get; set; }
        public DbSet<Waste> Wastes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierProducts> SuppliersProducts { get; set; }
    }
}
