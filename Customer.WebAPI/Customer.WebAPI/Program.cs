using Serilog;

namespace Customer.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
