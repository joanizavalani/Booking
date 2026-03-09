using Booking.Application.Features.Users.Login;
using Booking.Application.Features.Users.Register;
using Booking.Application.Features.Users.UpdateProfile;
using Booking.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("v1/users")]
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var authResponse =
                await _mediator.Send(command, cancellationToken);

            return Ok(authResponse);
        }
        catch (Exception e)
        {
            return Unauthorized(new
            {
                message = e.Message
            });
        }
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }

    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateUserProfileCommand command,
        CancellationToken cancellationToken)
    {
        var userId =
            await _mediator.Send(command, cancellationToken);

        return Ok(new {
            Id = userId
        });
    }
}