using Booking.Application.Contracts.Security;
using Booking.Application.Features.UserRoles;
using MediatR;

namespace Booking.Application.Features.Users.Login;

public class LoginUserCommandHandler
    : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUserRoleRepository _userRoleRepository;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<AuthResponse> Handle
        (LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user =
            await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user == null)
            throw new Exception("Invalid credentials.");

        if (!_passwordHasher.VerifyPassword(
                command.Password, user.PasswordHash))
            throw new Exception("Invalid credentials.");

        if (!user.IsActive)
            throw new Exception("Invalid credentials.");

        var userRoles =
            await _userRoleRepository.GetAllUserRolesToListAsync(user.Id);

        var tokenResult
            = _tokenService.GenerateToken(user, userRoles);

        return new AuthResponse(
            UserId: user.Id,
            AccessToken: tokenResult.AccessToken,
            ExpiresAt: tokenResult.ExpiresAt);
    }
}