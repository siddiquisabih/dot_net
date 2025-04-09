using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Castle.Facilities.Logging;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.Castle.Logging.Log4Net;
using Abp.Extensions;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Dependency;
using Abp.Json;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Hangfire;
using Newtonsoft.Json;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Connections;
using Global.Project.Hubs.NoficationHub;
using Global.Project.Identity;
using Global.Project.Configuration;


namespace Global.Project.Web.Host.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private const string _apiVersion = "v1";

        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                }
            );

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.AddSignalR();

            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(_apiVersion, new OpenApiInfo
                {
                    Version = _apiVersion,
                    Title = "Project API",
                    Description = "Project",
                    Contact = new OpenApiContact
                    {
                        Name = "Github",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/siddiquisabih"),
                    }
                });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.ToString());
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.AddMemoryCache();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(_appConfiguration.GetConnectionString("Default"));
            });
            services.AddHangfireServer();
            return services.AddAbp<ProjectWebHostModule>(
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            );

        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseCors(_defaultCorsPolicyName); 

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });


            app.UseRouting();

            app.UseAuthentication();

            app.UseAbpRequestLocalization();

            app.UseHangfireServer();

            app.UseHangfireDashboard("/project/background/jobs/dashboard");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalRNotificationHub>("/signalr/task-hub");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", $"Project API {_apiVersion}");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Global.Project.Web.Host.wwwroot.swagger.ui.index.html");
                options.DisplayRequestDuration();
            }); 
        }
    }
}
