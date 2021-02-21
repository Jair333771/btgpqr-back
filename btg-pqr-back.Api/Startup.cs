using btg_pqr_back.Infrastructure.Handlers;
using btg_pqr_back.Infrastructure.ServiceCollection;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace btg_pqr_back.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersAndNewtonsoft();
            services.AddSqlServerContext(Configuration);
            services.AddServices();
            services.AddHttpContextAccessor();
            services.AddMediator();
            services.AddAutoMapper();
            services.AddLogger(Configuration);
            services.AddSwaggerDoc(Configuration);
            services.AddCustomCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILog logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("mycors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Global Email");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
