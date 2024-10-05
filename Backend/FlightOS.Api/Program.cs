
using FlightOS.Api.Seeding;
using FlightOS.Application.DependencyInjection;
using FlightOS.Infrastructure.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FlightOS.Api
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments.</param>
        public static void Main(string[] args)
        {
            // Define application variables
            var builder = WebApplication.CreateBuilder(args);
            var Configuration = builder.Configuration;
            var environment = builder.Environment;
            var services = builder.Services;

            // Register Infrastructure services
            services.AddInfrastructureServices(Configuration);

            // Register Application services
            services.AddApplicationServices(Configuration);

            // Register Seeding services
            services.AddHostedService<UserRoleInitializer>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // Add Swagger generation with options
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FlightOS API",
                    Version = "v1",
                    Description = "API for FlightOS application",
                    Contact = new OpenApiContact
                    {
                        Name = "Luc Joosten",
                        Email = "lhajoosten@outlook.com",
                        Url = new Uri("https://flight-os.com")
                    }
                });

                // Include XML comments (optional)
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Add CORS Middleware
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Correct middleware order
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightOS API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
