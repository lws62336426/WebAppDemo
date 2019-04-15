using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppDemo
{
    public class EndpointTestMiddleware
    {
        private RequestDelegate _next;

        public EndpointTestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            if(endpoint==null)
            {
                await _next(httpContext);
                return;
            }
            var attributes = endpoint.Metadata.OfType<LogAttribute>();
            foreach (var attribute in attributes)
            {
                Debug.WriteLine("------------------------------------------------------------------------");
                Debug.WriteLine(attribute.Message);
                Debug.WriteLine("------------------------------------------------------------------------");
            }

            await _next(httpContext);
        }
    }

    public sealed class LogAttribute:Attribute
    {
        public LogAttribute(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
