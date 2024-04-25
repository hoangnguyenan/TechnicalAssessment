using Microsoft.EntityFrameworkCore;
using Customer.WebAPI.Configurations;

namespace Customer.WebAPI.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerConfiguration>()
                .HasKey(x => new { x.Id });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CustomerConfiguration> CustomerRequests { get; set; }
    }
}
