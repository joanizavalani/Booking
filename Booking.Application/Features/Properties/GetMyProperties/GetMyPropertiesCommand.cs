using MediatR;

namespace Booking.Application.Features.Properties.GetMyProperties;

public record GetMyPropertiesCommand
    : IRequest<List<PropertyDto>>;