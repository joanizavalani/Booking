using Booking.Application.Features.Reviews.DeleteReview;
using Booking.Application.Features.Reviews.GetPropertyReviews;
using Booking.Domain.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("v1/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Guest")]
    public async Task<IActionResult> CreateReview(
        [FromBody] CreateReviewDto dto,
        CancellationToken cancellationToken)
    {
        var reviewId = await _mediator.Send(
            new CreateReviewCommand(dto),
            cancellationToken);

        return CreatedAtAction(
            nameof(CreateReview),
            new { id = reviewId },
            new { Id = reviewId });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Guest")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        await _mediator.Send(new DeleteReviewCommand(id));

        return NoContent();
    }

    [HttpGet("property/{propertyId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByProperty(Guid propertyId)
    {
        var result = await _mediator.Send(
            new GetPropertyReviewsQuery(propertyId));

        return Ok(result);
    }
}