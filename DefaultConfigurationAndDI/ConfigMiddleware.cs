using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultConfigurationAndDI
{
    public class ConfigMiddleware
    {
        private readonly RequestDelegate _next;
        public IConfiguration AppConfiguration { get; set; }
        public ConfigMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            AppConfiguration = config;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
           await context.Response.WriteAsync($"<br><b>ConfigMiddleware:</b> name:{AppConfiguration["name"]},age: {AppConfiguration["age"]}<br>");
            await _next.Invoke(context);
        }

    }
}
