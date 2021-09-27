using System;
using System.Text.Json;
using System.Threading.Tasks;
using CopyPasta.Api.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CopyPasta.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                if (exception is BaseException customException)
                {
                    response.StatusCode = customException.StatusCode;
                }
                else
                {
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                var result = JsonSerializer.Serialize(new { message = exception?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}