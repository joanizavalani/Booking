using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Features.Roles;
using Booking.Domain.UserRoles;
using Booking.Domain.Users;
using MediatR;

namespace Booking.Application.Features.Users.Register;

public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRoleRepository _roleRepository;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
    }

    public async Task<Guid> Handle
        (RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(command.CreateUserDto.Email, cancellationToken);

        if (existingUser != null)
            throw new Exception("A user with this email already exists.");

        var passwordHash = _passwordHasher.HashPassword(
            command.CreateUserDto.Password);

        var user = User.CreateUser(
            command.CreateUserDto, passwordHash);

        var defaultRole = 
            await _roleRepository.GetDefaultRoleAsync(cancellationToken);

        if (defaultRole == null)
            throw new Exception("Default role not configured.");

        user.UserRoles.Add(
            UserRole.CreateUserRole(user.Id, defaultRole.Id));

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}