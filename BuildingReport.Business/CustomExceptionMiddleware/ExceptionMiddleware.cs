using BuildingReport.Business.Concrete;
using BuildingReport.Business.Logging.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext,ex);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var customExceptionType = typeof(CustomException);
            var customExceptionTypes = Assembly.GetAssembly(customExceptionType).GetTypes().Where(t => t.IsClass && !t.IsAbstract && customExceptionType.IsAssignableFrom(t));

            if (customExceptionTypes.Any(t => t.IsInstanceOfType(exception)))
            {
                var customException = (CustomException)exception;
                context.Response.StatusCode = (int)customException.StatusCode;

                var errorDetails = new ErrorDetails
                {
                    StatusCode = (int)customException.StatusCode,
                    Message = customException.Message,
                };

                return context.Response.WriteAsync(errorDetails.ToString());
            }
            else if (exception is ArgumentException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errorDetails = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid argument provided",
                };

                return context.Response.WriteAsync(errorDetails.ToString());
            }
            else if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var errorDetails = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Unauthorized access",
                };

                return context.Response.WriteAsync(errorDetails.ToString());
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorDetails = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal server error",
                };

                return context.Response.WriteAsync(errorDetails.ToString());
            }
        }












    }


}
