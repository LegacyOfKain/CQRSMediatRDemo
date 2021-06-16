using CQRSMediatRDemo.Behaviours;
using CQRSMediatRDemo.Database;
using CQRSMediatRDemo.Filters;
using CQRSMediatRDemo.Interfaces;
using CQRSMediatRDemo.Validation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CQRSMediatRDemo
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
            //If we want to use our filter globally,
            //we need to register it inside the AddControllers() method in the ConfigureServices method
            services.AddControllers(options=>options.Filters.Add(typeof(ResponseMappingFilter)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRSMediatRDemo", Version = "v1" });
            });

            services.AddSingleton<IRepository, Repository>();
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMemoryCache();

            // Add our validators - takes care of all validators implementing IValidationHandler
            services.AddValidators();

            //Order is important for IPipelineBehaviour
            // Pipeline1 then Pipleline2 means Pipeline1 will get executed first

            // <,> means <TRequest,TResponse>
            services.AddTransient(typeof(IPipelineBehavior<,>) , typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRSMediatRDemo v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
