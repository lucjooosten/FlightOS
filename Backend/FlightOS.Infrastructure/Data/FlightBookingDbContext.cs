using FlightOS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightOS.Infrastructure.Data
{
    public class FlightBookingDbContext : IdentityDbContext<ApplicationUser>
    {
        public FlightBookingDbContext(DbContextOptions<FlightBookingDbContext> options)
            : base(options)
        {
        }

        // DbSet for other domain entities
        // public DbSet<Flight> Flights { get; set; }
        // public DbSet<Reservation> Reservations { get; set; }
        // public DbSet<Payment> Payments { get; set; }

        // Additional configurations or model-building logic
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  // Required for Identity tables

            // Apply configurations
            //builder.ApplyConfiguration(new FlightConfigurations());
            //builder.ApplyConfiguration(new BookingConfigurations());
        }
    }
}
