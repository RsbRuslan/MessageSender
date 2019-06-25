using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageSender.Interfaces;
using MessageSender.Middleware;
using MessageSender.Models.Option;
using MessageSender.Repositories;
using MessageSender.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MessageSender
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<NotificationServiceConfig>(Configuration.GetSection("NotificationServiceConfiguration"));
            
            services.AddScoped(typeof(IDbService<>), typeof(LiteDbService<>));
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageStatusesRepository, MessageStatusesRepository>();


            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("AllowOrigin", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMvc();
            app.UseCors("default");
           
        }
    }
}
