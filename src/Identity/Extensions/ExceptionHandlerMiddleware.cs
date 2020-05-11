using System;
using System.Net;
using System.Threading.Tasks;
using Identity.Application.Exceptions;
using Identity.Core.Exceptions;
using Identity.Persistence.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Identity.Extensions
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
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var errorCode = "Unknown error";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "";
            var exceptionType = exception.GetType();
            switch(exception)
            {
                case { } e when exceptionType == typeof(UnauthorizedAccessException) 
                                || exceptionType == typeof(IdentityExceptions) 
                                || exceptionType == typeof(DomainException) 
                                || exceptionType == typeof(RepositoryException):
                    statusCode = HttpStatusCode.Unauthorized;
                    errorCode = "Unauthorized";
                    message = exception.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Nie intere, bo kici kici";
                    break;
            }
            
            var response = new { code = errorCode, message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}