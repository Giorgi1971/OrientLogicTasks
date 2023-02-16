using System;
using System.Text;
using Newtonsoft.Json;
using P_4_BonusManagement.Data;
using P_4_BonusManagement.Data.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace P_4_BonusManagement.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppDbContext _db;


    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, AppDbContext _db)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var error = new { message = ex.Message };
            var errorJson = JsonConvert.SerializeObject(error);
            httpContext.Response.StatusCode = 555;
            httpContext.Response.ContentType = "application/json";
            LogError(ex, _db);
            await httpContext.Response.WriteAsync(errorJson, Encoding.UTF8);
        }
    }

    public static void LogError(Exception ex, AppDbContext dbContext)
    {
        ErrorLogEntity error3 = new ErrorLogEntity()
        {
            ErrorMessage = ex.Message,
            StackTrace = ex.StackTrace,
            ErrorDate = DateTime.Now,
        };
        dbContext.ErrorLogEntities.Add(error3);
        dbContext.SaveChanges();

    }

}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    { return builder.UseMiddleware<ErrorHandlerMiddleware>(); }
}