using MediatR;

namespace Booking.Application.Features.Properties.GetProperty;

public record GetPropertyCommand(Guid PropertyId)
    : IRequest<PropertyDto>;