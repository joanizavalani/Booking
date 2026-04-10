using Booking.Application.Contracts;
using Booking.Domain.Properties;

namespace Booking.Application.Features.Properties;

public interface IPropertyRepository
    : IGenericRepository<Property>
{
}