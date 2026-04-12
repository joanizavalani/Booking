using Booking.Application.Features.Properties.CreateProperty;
using Booking.Domain.Users;
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

    [HttpPost("create-property")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> CreateProperty(
        [FromBody] CreatePropertyCommand command,
        CancellationToken cancellationToken)
    {
        var propertyId = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(CreateProperty),
            new { id = propertyId },
            new { Id = propertyId });
    }
}