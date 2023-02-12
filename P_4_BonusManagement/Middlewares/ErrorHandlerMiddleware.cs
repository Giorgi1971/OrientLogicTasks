using System.Text;
using Newtonsoft.Json;
using P_4_BonusManagement.Data;

namespace P_4_BonusManagement.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppDbContext _db;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            LogError(ex);
            var error = new { message = ex.Message };
            var errorJson = JsonConvert.SerializeObject(error);
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(errorJson, Encoding.UTF8);
        }
    }

    private void LogError(Exception exception)
    {
        Console.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss} [error] {exception}");
    }
}