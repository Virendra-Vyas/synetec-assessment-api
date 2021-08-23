using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Extension
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate @delegate;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            @delegate = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await @delegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            _logger.LogError($"Internal Server Error: {ex}");

            var error = new Error()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."
            };

            var result = JsonConvert.SerializeObject(error);

            return context.Response.WriteAsync(result);
        }
    }
}