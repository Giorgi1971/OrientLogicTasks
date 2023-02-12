using System;
using MovieDatabaseAPI.Data;

namespace MovieDatabaseAPI.ErrorSaver
{
    public static class ErrorLogHelper2
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
}
