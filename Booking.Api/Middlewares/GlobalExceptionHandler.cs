using Booking.Application.Exceptions;

namespace Booking.Api.Middlewares;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = e switch
        {
            BadRequestException
                => StatusCodes.Status400BadRequest,

            NotFoundException
                => StatusCodes.Status404NotFound,

            UnauthorizedException
                => StatusCodes.Status401Unauthorized,

            _ 
                => StatusCodes.Status500InternalServerError
        };

        var response = new
        {
            error = e.Message
        };

        return context.Response
            .WriteAsJsonAsync(response);
    }
}