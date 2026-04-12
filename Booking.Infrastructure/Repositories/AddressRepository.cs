using Booking.Application.Features.Addresses;
using Booking.Domain.Addresses;
using Booking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class AddressRepository
    : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(BookingDbContext dbContext)
        : base(dbContext) { }

    public async Task<Address?> GetByDetailsAsync(
        string country,
        string city,
        string street,
        string postalCode,
        CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(a =>
            a.Country == country &&
            a.City == city &&
            a.Street == street &&
            a.PostalCode == postalCode,
            cancellationToken);
    }
}