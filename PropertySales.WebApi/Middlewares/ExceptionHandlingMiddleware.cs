using System.Net;
using System.Text.Json;
using FluentValidation;
using PropertySales.Application.Common.Exceptions;
using PropertySales.SecureAuth.Exceptions;

namespace PropertySales.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
        }
        catch (CreateUserException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
        }
        catch (RecordExistsException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context,
        Exception exception, HttpStatusCode httpStatusCode)
    {
        HttpResponse response = context.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        var errorDto = new
        {
            Message = exception.Message,
            StatusCode = (int) httpStatusCode
        };

        string result = JsonSerializer.Serialize(errorDto);
        
        _logger.LogError(exception, $"Error - {exception}");

        await context.Response.WriteAsync(result);
    }
}