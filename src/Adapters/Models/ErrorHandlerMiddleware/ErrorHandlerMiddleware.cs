using System.Net;
using System.Text.Json;

namespace Inferno.src.Adapters.Models.ErrorHandlerMiddleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exeption has occured. ");

                context.Response.ContentType = "Application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    Title = "An unexpected error ocurred.",
                    Status = context.Response.StatusCode,
                    Detail = context.RequestServices.GetService(typeof(IWebHostEnvironment))
                        is IWebHostEnvironment env
                    && env.IsDevelopment()
                        ? ex.Message
                        : "Please contact support",
                };

                var jsonResponse = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
