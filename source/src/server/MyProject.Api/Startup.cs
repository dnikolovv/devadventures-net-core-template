using AutoMapper;
using MyProject.Api.Configuration;
using MyProject.Api.Filters;
using MyProject.Api.ModelBinders;
using MyProject.Business.Identity;
using MyProject.Business.Services;
using MyProject.Core.Configuration;
using MyProject.Core.Identity;
using MyProject.Core.Services;
using MyProject.Data.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MyProject.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext(Configuration.GetConnectionString("DbConnectionString"));
            services.AddAutoMapper();
            services.AddSwagger();
            services.AddJwtIdentity(Configuration.GetSection(nameof(JwtConfiguration)));

            services.AddLogging(logBuilder => logBuilder.AddSerilog(dispose: true));

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IJwtFactory, JwtFactory>();

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new OptionModelBinderProvider());
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ModelStateFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                dbContext.Database.EnsureCreated();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddLogging(Configuration.GetSection("Logging"));

            app.UseHttpsRedirection();
            app.UseSwagger("My Web API.");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
