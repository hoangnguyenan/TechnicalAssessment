
namespace Customer.WebAPI.Extensions
{
    public static class AllowOriginsExtension
    {
        public static IServiceCollection AddCrossOrigins(this IServiceCollection services,
            IConfiguration configuration)
        {
            string domain = configuration.GetValue<string>("AllowCrossDomain");

            services.AddCors(o => o.AddPolicy("AllowAllPolicy", builder =>
            {
                builder.WithOrigins(domain.Split(","))
                            .WithExposedHeaders("Content-Disposition")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
            }));

            return services;
        }

        public static IApplicationBuilder UseCrossOrigins(this IApplicationBuilder app)
        {
            app.UseCors("AllowAllPolicy");

            return app;
        }
    }
}