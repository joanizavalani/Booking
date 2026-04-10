using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using MediatR;

namespace Booking.Application.Features.Users.UpdateProfile;

public class UpdateUserProfileCommandHandler
    : IRequestHandler<UpdateUserProfileCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserProfileCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var user =
            await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        user.UpdateUser(
            command.FirstName, command.LastName, command.PhoneNumber);

        user.UpdateModificationTime();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}