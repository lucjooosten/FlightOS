using FlightOS.Application.Interfaces;
using FlightOS.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightOS.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Services
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            // Add Automapper
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add FluentValidation
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
