using Booking.Domain.Users.Dtos;

namespace Booking.Application.Features.Users.Register;

public class RegisterUserCommand
{
    public CreateUserDto CreateUserDto { get; set; }

    public RegisterUserCommand(CreateUserDto createUserDto)
    {
        CreateUserDto = createUserDto;
    }
}