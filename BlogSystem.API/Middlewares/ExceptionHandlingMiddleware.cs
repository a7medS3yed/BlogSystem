using System.Net;
using System.Text.Json;

namespace BlogSystem.API.Middlewares
{
    public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger,
        RequestDelegate request)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await request(context); // Execute next middleware
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Unhandled exception: {e.Message}");
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var response = new
            {
                success = false,
                message = e.Message,
                details = e.InnerException?.Message ?? string.Empty,
                stackTrace = e.StackTrace
            };

            var json = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            int statusCode = e switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = statusCode;
            

            return context.Response.WriteAsync(json);
        }
    }
    }
