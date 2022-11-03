using Microsoft.EntityFrameworkCore;
using Product.Microservice.Model;

namespace Product.Microservice.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {this.Database.EnsureCreated();}
        public DbSet<ProductItem> ProductItems { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
