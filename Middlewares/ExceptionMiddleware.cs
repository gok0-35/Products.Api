using System.Net;
using System.Text.Json;
using Products.Api.Exceptions;
using Products.Api.Responses;

namespace Products.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException ex)
        {
            _logger.LogWarning(ex,
                "Handled API exception. TraceId: {TraceId}",
                context.TraceIdentifier);

            await HandleApiExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unhandled exception. TraceId: {TraceId}",
                context.TraceIdentifier);

            await HandleUnhandledExceptionAsync(context, ex);
        }
    }

    private async Task HandleApiExceptionAsync(HttpContext context, ApiException ex)
    {
        context.Response.StatusCode = ex.StatusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new ApiErrorResponse
        {
            StatusCode = ex.StatusCode,
            Title = GetTitle(ex.StatusCode),
            Message = ex.Message,
            TraceId = context.TraceIdentifier,
            Timestamp = DateTime.UtcNow
        };

        await WriteResponseAsync(context, errorResponse);
    }

    private async Task HandleUnhandledExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new ApiErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Title = "Internal Server Error",
            Message = _env.IsDevelopment()
                ? ex.Message
                : "Beklenmeyen bir hata oluştu.",
            TraceId = context.TraceIdentifier,
            Timestamp = DateTime.UtcNow
        };

        await WriteResponseAsync(context, errorResponse);
    }

    private static async Task WriteResponseAsync(
        HttpContext context,
        ApiErrorResponse errorResponse)
    {
        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }

    private static string GetTitle(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => "Bad Request",
            StatusCodes.Status404NotFound => "Not Found",
            StatusCodes.Status401Unauthorized => "Unauthorized",
            StatusCodes.Status403Forbidden => "Forbidden",
            _ => "Error"
        };
    }
}
