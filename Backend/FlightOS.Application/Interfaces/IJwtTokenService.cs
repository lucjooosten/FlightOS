using FlightOS.Domain.Entities;

namespace FlightOS.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(ApplicationUser user, IList<string> roles);
    }
}
