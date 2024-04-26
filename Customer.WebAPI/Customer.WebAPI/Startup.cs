using System.Reflection;
using Customer.WebAPI.Extensions;
using Customer.WebAPI.Infrastructures;
using Customer.WebAPI.Repositories;
using Customer.WebAPI.Services;
using Serilog;

namespace Customer.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            AddServices(services);

            AddRepositories(services);

            services.AddInfrastructure(Configuration);

            services.AddSwashbuckle();

            services.AddHealthChecks();

            services.AddCrossOrigins(Configuration);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICustomerService, CustomerService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<MigrationDatabase>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseCrossOrigins();

            app.UseSwashbuckle();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
