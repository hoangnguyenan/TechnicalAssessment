using Customer.WebAPI.Infrastructures;
using Serilog;

namespace Customer.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                var logger = loggerFactory.CreateLogger<Program>();

                try
                {
                    logger.LogInformation("Start EF appling migration...");

                    var migrationDatabase = services.GetRequiredService<MigrationDatabase>();

                    migrationDatabase.Migrate();                    

                    logger.LogInformation("End EF appling migration...");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migration the database.");
                    
                    throw;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostContext, options) =>
                   {
                       options.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                              optional: true, reloadOnChange: true);

                       options.AddEnvironmentVariables();

                       options.AddCommandLine(args);
                   });
                   
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom
                                .Configuration(hostingContext.Configuration).Enrich.FromLogContext();
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddSerilog();
                });
    }
}
