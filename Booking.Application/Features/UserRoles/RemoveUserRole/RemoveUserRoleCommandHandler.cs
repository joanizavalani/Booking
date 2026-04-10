using Booking.Application.Contracts;
using Booking.Application.Exceptions;
using Booking.Application.Features.Roles;
using Booking.Application.Features.Users;
using Booking.Domain.Roles;
using MediatR;

namespace Booking.Application.Features.UserRoles.RemoveUserRole;

public class RemoveUserRoleCommandHandler
    : IRequestHandler<RemoveUserRoleCommand>
{
    private readonly IUserRoleRepository _userRoleRepository;

    private readonly IUserRepository _userRepository;

    private readonly IRoleRepository _roleRepository;

    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserRoleCommandHandler(
        IUserRoleRepository userRoleRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork)
    {
        _userRoleRepository = userRoleRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveUserRoleCommand command, CancellationToken cancellationToken)
    {
        var normalizedRoleName = command.RoleName.Trim();

        if (string.Equals(
                normalizedRoleName, RoleNames.Guest,
                StringComparison.OrdinalIgnoreCase))
            throw new BadRequestException("Default role cannot be removed.");

        if (string.Equals(
                normalizedRoleName, RoleNames.Admin,
                StringComparison.OrdinalIgnoreCase))
            throw new BadRequestException("Admin role cannot be removed.");

        var user =
            await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        var role =
            await _roleRepository.GetRoleByNameAsync(command.RoleName, cancellationToken);

        if (role == null)
            throw new NotFoundException("Role not found.");

        var userRole =
            await _userRoleRepository.GetByUserIdAndRoleIdAsync(user.Id, role.Id, cancellationToken);

        if (userRole == null)
            throw new NotFoundException("User role not found.");

        _userRoleRepository.Delete(userRole);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}