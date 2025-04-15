using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.CustomExceptions;
using System.Data.Common;
using System.Text.Json;

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
            await HandleError(context, ex);
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
            RequestException requestException =>
                new
                {
                    code = StatusCodes.Status400BadRequest,
                    message = requestException.Description
                },
            DbUpdateException dbUpdateEx when dbUpdateEx.InnerException is DbException dbEx && dbEx.Message.Contains("IX_AspNetUsers_PhoneNumber") => 
                new
                {
                    code = StatusCodes.Status400BadRequest,
                    message = ErrorMessages.PhoneNumberExists
                },
            _ => 
                new
                {
                    code = 500,
                    message = "Щось пішло не так"
                }
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = responseErrorMessage.code;

        await context.Response.WriteAsync(JsonSerializer.Serialize(responseErrorMessage));
    }
}
