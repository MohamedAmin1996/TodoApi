using FluentValidation;
using System.Net;
using System.Text.Json;
using TodoApi.Application.Common.Exceptions;
using TodoApi.Domain.Exceptions;


namespace TodoApi.API.Middleware;


// Single Responsibility: this class handles ALL exceptions in one place.
// Returns RFC 7807 ProblemDetails format.
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;


    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    { _next = next; _logger = logger; }


    public async Task Invoke(HttpContext ctx)
    {
        try { await _next(ctx); }
        catch (Exception ex) { await HandleAsync(ctx, ex); }
    }


    private async Task HandleAsync(HttpContext ctx, Exception ex)
    {
        var (status, title) = ex switch
        {
            NotFoundException => (HttpStatusCode.NotFound, ex.Message),
            ValidationException => (HttpStatusCode.BadRequest, "Validation failed."),
            UnauthorizedException => (HttpStatusCode.Unauthorized, ex.Message),
            BusinessRuleException => (HttpStatusCode.UnprocessableEntity, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };


        _logger.LogError(ex, "Exception: {Message}", ex.Message);


        ctx.Response.StatusCode = (int)status;
        ctx.Response.ContentType = "application/problem+json";


        var detail = ex is ValidationException ve
            ? string.Join("; ", ve.Errors.Select(e => e.ErrorMessage))
            : ex.Message;


        var problem = new { title, detail, status = (int)status };
        await ctx.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}
