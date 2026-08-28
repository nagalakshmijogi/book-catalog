using System.Text.Json;

namespace BookCatalog.api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType ="application/json";

                var response = new
                {
                    status = 500,
                    message = "An unexpected error occurred."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}