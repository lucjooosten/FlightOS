using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightOS.Infrastructure.Configurations
{
    /*
    public class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BookingNumber).IsRequired();
            builder.Property(b => b.BookingDate).IsRequired();
            builder.Property(b => b.PassengerName).IsRequired();
            builder.Property(b => b.PassengerEmail).IsRequired();
            builder.Property(b => b.PassengerPhone).IsRequired();
            builder.Property(b => b.PassengerAddress).IsRequired();
            builder.Property(b => b.TotalPrice).IsRequired();
        }
    }
    */
}