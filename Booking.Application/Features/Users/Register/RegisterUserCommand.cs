using Booking.Domain.Users;
using MediatR;

namespace Booking.Application.Features.Users.Register;

public class RegisterUserCommand
    : IRequest<Guid>
{
    public CreateUserDto CreateUserDto { get; set; }

    public RegisterUserCommand(CreateUserDto createUserDto)
    {
        CreateUserDto = createUserDto;
    }
}