using Booking.Application.Features.Bookings.CancelBooking;
using Booking.Application.Features.Bookings.CompleteBooking;
using Booking.Application.Features.Bookings.ConfirmBooking;
using Booking.Application.Features.Bookings.CreateBooking;
using Booking.Domain.Bookings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("v1/bookings")]
public class BookingController
    : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Guest")]
    public async Task<IActionResult> CreateBooking(
    [FromBody] CreateBookingDto dto,
    CancellationToken cancellationToken)
    {
        var bookingId = await _mediator.Send(
            new CreateBookingCommand(dto),
            cancellationToken);

        return CreatedAtAction(
            nameof(CreateBooking),
            new { id = bookingId },
            new { Id = bookingId });
    }

    [HttpPost("{id}/confirm")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> ConfirmBooking(Guid id)
    {
        await _mediator.Send(new ConfirmBookingCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/reject")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> RejectBooking(Guid id)
    {
        await _mediator.Send(new RejectBookingCommand { BookingId = id });
        return NoContent();
    }

    [HttpPost("{id}/cancel")]
    [Authorize(Roles = "Owner,Guest")]
    public async Task<IActionResult> CancelBooking(Guid id)
    {
        await _mediator.Send(new CancelBookingCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/complete")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> CompleteBooking(Guid id)
    {
        await _mediator.Send(new CompleteBookingCommand(id));
        return NoContent();
    }
}