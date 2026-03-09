using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using MediatR;

namespace Booking.Application.Features.Users.ChangePassword;

public class ChangePasswordCommandHandler
    : IRequestHandler<ChangePasswordCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public ChangePasswordCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var user =
            await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null)
            throw new NotFoundException("User not found.");

        if (!_passwordHasher.VerifyPassword(command.OldPassword, user.PasswordHash))
            throw new BadRequestException("Old password is incorrect.");

        user.UpdatePassword(
            _passwordHasher.HashPassword(command.NewPassword)
        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}