using Microsoft.AspNetCore.Http;
using Reabilit.Domain.CustomExceptions;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Reabilit.Domain.CustomMiddlewares;

public class ExceptionHandlingMiddleware
{
    public readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        } 
        catch (Exception ex)
        {
            HandleError(context, ex);
        }
    }

    public async Task HandleError(HttpContext context, Exception exception)
    {
        var responseErrorMessage = exception switch
        {
            NotFoundException notFoundException =>
                new {
                    code = StatusCodes.Status404NotFound,
                    message = notFoundException.Description
                },

            _ => new
            {
                code = 0,
                message = "Щось пішло не так"
            }
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = responseErrorMessage.code;

        await context.Response.WriteAsync(JsonSerializer.Serialize(responseErrorMessage));
    }
}
