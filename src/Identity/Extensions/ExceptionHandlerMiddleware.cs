using System;
using System.Net;
using System.Threading.Tasks;
using Identity.Application.Exceptions;
using Identity.Core.Exceptions;
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

            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest; 
            var exceptionType = exception.GetType();
            switch(exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    errorCode = "Unauthorized";
                    break;
                case Exception e when exceptionType == typeof(IdentityExceptions):
                    statusCode = HttpStatusCode.Unauthorized;
                    errorCode = "Unauthorized";
                    break;
                case Exception e when exceptionType == typeof(DomainException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Message;
                    break;    
                case Exception e when  exceptionType == typeof(RepositoryException):
                    statusCode = HttpStatusCode.NoContent;
                    errorCode = "No Content";
                    break;
                case Exception e when exceptionType == typeof(Exception):
                    statusCode = HttpStatusCode.InternalServerError;
                    errorCode = "Nie intere, bo kici kici";
                    break;                       
            }
            
            var response = new { code = errorCode, message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}