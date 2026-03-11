using MediatR;

namespace Booking.Application.Features.Users.DeleteAccount;

public record DeleteAccountCommand
    : IRequest;