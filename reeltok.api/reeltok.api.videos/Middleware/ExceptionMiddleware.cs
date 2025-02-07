using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using reeltok.api.videos.DTOs;

namespace reeltok.api.videos.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has occurred: {ex}");
                await HandleExceptionAsync(context);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/xml";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            FailureResponseDto response = new FailureResponseDto("public server error!");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(FailureResponseDto));
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, response);
                string responseXml = stringWriter.ToString();
                return context.Response.WriteAsync(responseXml);
            }
        }
    }
}
