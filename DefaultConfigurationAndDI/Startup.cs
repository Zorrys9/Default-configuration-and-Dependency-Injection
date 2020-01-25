using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DefaultConfigurationAndDI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            string[] args = { "name=Bob" };
            var builder = new ConfigurationBuilder()
                 .AddCommandLine(args)
                 .AddInMemoryCollection(new Dictionary<string, string>
                 {
                    {"age","19" }
                 })
                 .AddConfiguration(config);
            AppConfiguration = builder.Build();
        }
        public IConfiguration AppConfiguration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfiguration>(provider => AppConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ConfigMiddleware>();
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"name {AppConfiguration["name"]}, age:{AppConfiguration["age"]}");
            });
        }
    }
}
