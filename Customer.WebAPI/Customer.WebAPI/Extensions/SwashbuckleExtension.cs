
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Customer.WebAPI.Extensions
{
    public static class SwashbuckleExtension
    {
        public static IServiceCollection AddSwashbuckle(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Heineken.WebApi", Version = "v1" });

                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
        public static IApplicationBuilder UseSwashbuckle(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Heineken.WebApi v1"));

            return app;
        }
    }
}