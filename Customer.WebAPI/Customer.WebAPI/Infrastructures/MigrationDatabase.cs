
using Customer.WebAPI.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Customer.WebAPI.Infrastructures
{
    public class MigrationDatabase
    {
        private readonly DatabaseContext _context;

        public IConfiguration Configuration { get; }

        public MigrationDatabase(DatabaseContext context,
            IConfiguration configuration)
        {
            _context = context;

            Configuration = configuration;
        }

        public void Migrate()
        {
            string connectionString = Configuration.GetConnectionString("TestConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new NullReferenceException("TestConnection Connection string should not empty");
            }

            _context.Database.Migrate();
        }
    }
}