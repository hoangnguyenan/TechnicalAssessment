
using Microsoft.EntityFrameworkCore;
using Customer.WebAPI.DbContexts;

namespace Customer.WebAPI.Infrastructures
{
    public static class SqlContext
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("TestConnection");

            if(string.IsNullOrEmpty(connectionString))
            {
                throw new NullReferenceException("TestConnection Connection string should not empty");
            }

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}