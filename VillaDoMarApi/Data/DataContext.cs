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
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<StatusCaixa> StatusCaixas { get; set; }

    }
}
