using Kicks.Models.Exceptions;
using Kicks.Services.Exceptions.BadRequest;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Kicks.Services.Exceptions.Middleware
{
    public class KicksExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public KicksExceptionMiddleware(RequestDelegate next)
        {
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            string message = exception.Message;

            switch (exception)
            {
                case KicksBadRequestException badRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = badRequestException.Message;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            var errorResponse = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message,
                Details = exception.StackTrace // Optional, you might want to remove this in production
            };

            var errorJson = JsonConvert.SerializeObject(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(errorJson);
        }
    }
}
