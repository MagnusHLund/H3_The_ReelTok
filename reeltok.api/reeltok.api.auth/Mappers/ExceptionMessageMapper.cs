using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace reeltok.api.auth.Mappers
{
    internal static class ExceptionMessageMapper
    {
        private static readonly Dictionary<Type, (string responseMessage, HttpStatusCode responseStatusCode)> ExceptionMessages = new Dictionary<Type, (string, HttpStatusCode)>
        {
            { typeof(ArgumentException), ("An argument failed validation", HttpStatusCode.BadRequest) },
            { typeof(DbUpdateException), ("Internal server error.", HttpStatusCode.InternalServerError) },
            { typeof(UnauthorizedAccessException), ("Invalid credentials.", HttpStatusCode.Unauthorized) },
            { typeof(ArgumentNullException), ("A required argument was null.", HttpStatusCode.BadRequest) },
            { typeof(KeyNotFoundException), ("The requested resource was not found.", HttpStatusCode.NotFound) },
            { typeof(InvalidOperationException), ("An invalid operation was attempted.", HttpStatusCode.BadRequest) },
            {typeof(ValidationException), ("Password does not follow the minimum requirements!", HttpStatusCode.BadRequest)}
        };

        internal static (string responseMessage, HttpStatusCode responseStatusCode) GetExceptionDetails(Exception exception)
        {
            return ExceptionMessages.ContainsKey(exception.GetType())
                ? ExceptionMessages[exception.GetType()]
                : ("An unexpected error occurred.", HttpStatusCode.InternalServerError);
        }

        internal static string GetLogMessage(Exception exception)
        {
            return exception.Message ?? $"No log message provided. Exception type: {exception.GetType()}";
        }
    }
}