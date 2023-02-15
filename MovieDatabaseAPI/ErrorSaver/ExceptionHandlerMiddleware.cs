using System;
using MovieDatabaseAPI.Data;
using System.Text;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MovieDatabaseAPI.ErrorSaver
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GGGIiiioooorgi {DateTime.Now:dd-MM-yyyy HH:mm:ss} [error] {ex}");

                // Log the exception using a logging library of your choice
                LogException(ex);

                // Re-throw the exception so that it can be handled by the
                // global exception handler in Startup.cs
                throw;
            }
        }

        private void LogException(Exception ex)
        {
            // Implement logging logic here
            // e.g. using Microsoft.Extensions.Logging
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

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
                ErrorLogHelper.LogError(ex, _db);
                LogError(ex);
                var error = new { message = ex.Message };
                var errorJson = JsonConvert.SerializeObject(error);
                httpContext.Response.StatusCode = 523;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(errorJson, Encoding.UTF8);
            }
        }

        public static class ErrorLogHelper
        {
            public static void LogError(Exception ex, AppDbContext dbContext)
            {
                ErrorLog error = new ErrorLog
                {
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    ErrorDate = DateTime.Now
                };
                dbContext.ErrorLogs.Add(error);
                dbContext.SaveChanges();
            }
        }

        private void LogError(Exception exception)
        {
            Console.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss} [error] {exception}");
        }
    }
}

