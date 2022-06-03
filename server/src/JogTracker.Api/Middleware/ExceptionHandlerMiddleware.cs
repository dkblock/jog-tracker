using JogTracker.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JogTracker.Api.Middleware
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
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = exception.Message;
            var errors = new List<Error>();

            if (exception is ApiException apiException)
            {
                statusCode = apiException.StatusCode;

                if (apiException is BadRequestException badRequestException)
                    errors = badRequestException.Errors.ToList();
            }

            var responseBody = JsonConvert.SerializeObject(new { errors, message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(responseBody);
        }
    }
}
