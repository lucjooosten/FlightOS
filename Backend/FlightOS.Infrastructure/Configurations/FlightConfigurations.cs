using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightOS.Infrastructure.Configurations
{
    /*
    public class FlightConfigurations : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.FlightNumber).IsRequired();
            builder.Property(f => f.Departure).IsRequired();
            builder.Property(f => f.Destination).IsRequired();
            builder.Property(f => f.DepartureTime).IsRequired();
            builder.Property(f => f.ArrivalTime).IsRequired();
            builder.Property(f => f.Price).IsRequired();
        }
    }
    */
}