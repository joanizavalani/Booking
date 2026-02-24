using Booking.Application.Features.Users.Register;
using Booking.Domain.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserController
    : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(createUserDto);

        var userId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(Register),
            new { id = userId },
            new { Id = userId});
    }
}
