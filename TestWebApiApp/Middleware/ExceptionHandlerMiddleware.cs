using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace TestWebApiApp.Middleware
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
                    // Log the exception
                    Log.Error(ex, "An unexpected error occurred.");

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var response = new { message = "An internal server error occurred." };
                    var jsonResponse = JsonConvert.SerializeObject(response);
                    await context.Response.WriteAsync(jsonResponse);
                }
            }
        }
}
