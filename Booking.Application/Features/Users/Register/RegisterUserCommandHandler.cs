using Booking.Application.Contracts;
using Booking.Domain.Users;
using MediatR;

namespace Booking.Application.Features.Users.Register;

public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(command.CreateUserDto.Email);

        if (existingUser != null)
            throw new Exception("A user with this email already exists.");

        var passwordHash =
            BCrypt.Net.BCrypt.HashPassword(command.CreateUserDto.Password);

        var user = User.CreateUser(
            command.CreateUserDto, passwordHash);

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}