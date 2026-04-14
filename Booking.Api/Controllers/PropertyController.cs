using Booking.Application.Features.Properties.CreateProperty;
using Booking.Application.Features.Properties.GetMyProperties;
using Booking.Application.Features.Properties.GetProperty;
using Booking.Application.Features.Properties.UpdateProperty;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("v1/properties")]
public class PropertyController
    : ControllerBase
{
    private readonly IMediator _mediator;

    public PropertyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> CreateProperty(
        [FromBody] CreatePropertyCommand command,
        CancellationToken cancellationToken)
    {
        var propertyId =
            await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(CreateProperty),
            new { id = propertyId },
            new { Id = propertyId });
    }

    [HttpPut("update")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> UpdateProperty(
        [FromBody] UpdatePropertyCommand command,
        CancellationToken cancellationToken)
    {
        var propertyId =
            await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            Id = propertyId
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProperty(
    Guid id,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetPropertyCommand(id), cancellationToken);

        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> GetMyProperties(
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetMyPropertiesCommand(), cancellationToken);

        return Ok(result);
    }
}