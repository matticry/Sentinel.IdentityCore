using System.Net;
using System.Text.Json;
using Sentinel.Identity.Application.Commands;
using Sentinel.Identity.Domain.Exceptions;

namespace Sentinel.Identity.Api.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            ValidationException validationEx => CreateResponse(
                HttpStatusCode.BadRequest,
                validationEx.Message,
                validationEx.ErrorCode
            ),
            NotFoundException notFoundEx => CreateResponse(
                HttpStatusCode.NotFound,
                notFoundEx.Message,
                notFoundEx.ErrorCode
            ),
            BadRequestException badRequestEx => CreateResponse(
                HttpStatusCode.BadRequest,
                badRequestEx.Message,
                badRequestEx.ErrorCode
            ),
            DomainException domainEx => CreateResponse(
                HttpStatusCode.UnprocessableEntity,
                domainEx.Message,
                domainEx.ErrorCode
            ),
            _ => CreateResponse(
                HttpStatusCode.InternalServerError,
                "An internal server error occurred",
                "INTERNAL_ERROR"
            )
        };

        context.Response.StatusCode = (int)response.StatusCode;

        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var jsonResponse = JsonSerializer.Serialize(response.ApiResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }

    private (HttpStatusCode StatusCode, ApiResponse<object> ApiResponse) CreateResponse(
        HttpStatusCode statusCode,
        string message,
        string errorCode)
    {
        return (statusCode, ApiResponse<object>.ErrorResponse(message, errorCode));
    }
}