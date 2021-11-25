using Dwh.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dwh.Common.Mvc
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object response;

            if (exception is DomainException domainException)
            {
                context.Response.StatusCode = domainException.StatusCode;

                response = new
                {
                    code = domainException.ErrorCode,
                    error = domainException.Error,
                    message = exception.Message
                };
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                response = new
                {
                    code = 500,
                    error = exception.Message,
                    message = exception.Message,
                    trace = exception.StackTrace,
                    inner = exception.InnerException != null ? exception.InnerException.ToString() : ""
                };
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
