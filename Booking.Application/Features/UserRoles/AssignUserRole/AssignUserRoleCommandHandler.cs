using Booking.Application.Contracts;
using Booking.Application.Exceptions;
using Booking.Application.Features.Roles;
using Booking.Application.Features.Users;
using Booking.Domain.UserRoles;
using MediatR;

namespace Booking.Application.Features.UserRoles.AssignUserRole;

public class AssignUserRoleCommandHandler
    :IRequestHandler<AssignUserRoleCommand>
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserRoleCommandHandler(
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

    public async Task Handle(AssignUserRoleCommand command, CancellationToken cancellationToken)
    {
        var user =
            await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        var role =
            await _roleRepository.GetRoleByNameAsync(command.RoleName, cancellationToken);

        if (role == null)
            throw new NotFoundException("Role not found.");

        var alreadyAssigned =
            await _userRoleRepository.ExistsAsync(user.Id, role.Id, cancellationToken);

        if (alreadyAssigned)
            throw new BadRequestException("User already has this role.");

        var userRole = UserRole.CreateUserRole(
                user.Id, role.Id);

        await _userRoleRepository.AddAsync(userRole, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}