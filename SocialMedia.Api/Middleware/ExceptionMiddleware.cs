using System.Net.Mime;
using FluentValidation;
using SocialMedia.Application.Common;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Exceptions;

namespace SocialMedia.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.CacheControl = AppConstants.CacheControlValue;
        context.Response.Headers.Pragma = AppConstants.PragmaValue;
        context.Response.Headers.Expires = AppConstants.HeaderExpiresValue;

        try
        {
            await _next(context);
        }
        catch (ValidationException validationEx)
        {
            _logger.LogWarning(validationEx, Messages.ValidationFailed);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var errors = validationEx.Errors
                .Select(e => new ApiError
                {
                    Field = string.IsNullOrWhiteSpace(e.PropertyName) ? "General" : e.PropertyName,
                    Message = e.ErrorMessage
                })
                .ToList();

            var response = new ApiResponse<string>
            {
                Success = false,
                Messages = new List<string> { Messages.ValidationFailed },
                Errors = errors
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (AuthenticationException authEx)
        {
            // ✅ Handle login failures globally
            _logger.LogWarning(authEx, "Authentication failed");

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var response = new ApiResponse<string>
            {
                Success = false,
                Messages = new List<string> { "Authentication failed" },
                Errors = new List<ApiError>
                {
                    new() { Field = authEx.Field, Message = authEx.Message }
                }
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, Messages.UnexpectedError);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var response = new ApiResponse<string>
            {
                Success = false,
                Messages = new List<string> { Messages.UnexpectedError },
                Errors = new List<ApiError>
                {
                    new() { Field = "General", Message = ex.Message }
                }
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
