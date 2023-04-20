using MovieDatabaseAPI.Data;
using System.Text;
using Serilog;
using Newtonsoft.Json;

namespace MovieDatabaseAPI.ErrorSaver
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public async Task InvokeAsync(HttpContext httpContext, AppDbContext Db)
        {
            try
            {

                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                Log.CloseAndFlush();
                LogError(ex, Db);
                LogErrorInConsole(ex);
                var error = new { message = ex.Message };
                var errorJson = JsonConvert.SerializeObject(error);
                httpContext.Response.StatusCode = 523;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(errorJson, Encoding.UTF8);
            }
        }

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

        private static void LogErrorInConsole(Exception exception)
        {
            Console.WriteLine($"{DateTime.Now:dd-MM-yyyy HH:mm:ss} [error] {exception}");
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        { return builder.UseMiddleware<ErrorHandlerMiddleware>(); }
    }
}
