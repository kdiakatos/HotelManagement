using System.Net.Mime;

namespace Anixe
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger,
                                          RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong").ConfigureAwait(false);
            }
        }
    }
}
