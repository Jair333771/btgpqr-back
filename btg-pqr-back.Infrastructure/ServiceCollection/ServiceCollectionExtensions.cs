using btg_pqr_back.Common.Globals;
using btg_pqr_back.Common.Interfaces.Globals;
using btg_pqr_back.Core.Commands;
using btg_pqr_back.Core.Entities;
using btg_pqr_back.Core.Interfaces.Repository;
using btg_pqr_back.Infrastructure.Context;
using btg_pqr_back.Infrastructure.Mappers;
using btg_pqr_back.Infrastructure.Repositories;
using log4net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace btg_pqr_back.Infrastructure.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               opt => opt.MigrationsAssembly("btg-pqr-back.Infrastructure")));
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient(typeof(IRepository<>), typeof(Repository<>))
                           .AddTransient<IPqrRepository<PqrEntity>, PqrRepository>()
                           .AddTransient<IClaimRepository<ClaimEntity>, ClaimRepository>()
                           .AddTransient(typeof(IGlobalResponse<>), typeof(GlobalResponse<>));
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(CreatePqrCommand).Assembly);
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(AutoMappers).Assembly);
        }

        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddLogging(config =>
            {
                config.ClearProviders();
                config.AddConsole();
                config.AddConfiguration(Configuration.GetSection("Log4net"));
                config.SetMinimumLevel(LogLevel.Debug);
                config.AddLog4Net();
            });

            services.AddSingleton(factory =>
                LogManager.GetLogger(Configuration.GetValue<string>("Log4net:Name")));

            return services;
        }

        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, IConfiguration Configuration)
        {
            return services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = Configuration.GetValue<string>("ProjectApi:Title"),
                    Version = Configuration.GetValue<string>("ProjectApi:Version"),
                    Description = Configuration.GetValue<string>("ProjectApi:Description"),
                    Contact = new OpenApiContact
                    {
                        Email = Configuration.GetValue<string>("ProjectApi:Email"),
                        Name = Configuration.GetValue<string>("ProjectApi:Name")
                    }
                });

                var xmlFile = $"{AppDomain.CurrentDomain.Load("btg-pqr-back.Api").GetName().Name}.xml";

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                doc.IncludeXmlComments(xmlPath);
            });
        }

        public static IServiceCollection AddControllersAndNewtonsoft(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            return services;
        }
    }
}
