using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Extensions;
using Castle.Facilities.Logging;
using Global.Project.Authentication.JwtBearer;
using Global.Project.Binders;
using Global.Project.Common.Helpers;
using Global.Project.Configuration;
using Global.Project.Hubs.NoficationHub;
using Global.Project.Identity;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
            });

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new QueryFilterBinderProvider());
            });

            FileHelper.Initialize(_appConfiguration);
            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, builder =>
                {
                    builder.WithOrigins(_appConfiguration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(_apiVersion, new OpenApiInfo
                {
                    Version = _apiVersion,
                    Title = "Project API",
                    Description = "Project",
                    Contact = new OpenApiContact
                    {
                        Name = "Project",
                        Email = string.Empty,
                        Url = new Uri("https://Project.com"),
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

            return services.AddAbp<ProjectWebHostModule>(options =>
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(f =>
                    f.UseAbpLog4Net().WithConfig("log4net.config")));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; });

            app.UseCors(_defaultCorsPolicyName);
            app.UseStaticFiles(); // Serve wwwroot by default

            // Serve /Resources
            var resourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
            if (!Directory.Exists(resourcesPath))
                Directory.CreateDirectory(resourcesPath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(resourcesPath),
                RequestPath = "/Resources"
            });

            // Serve /storage (upload folder)
            var uploadSetting = _appConfiguration.GetValue<string>("Paths:FileUploadPath");
            var uploadPath = string.IsNullOrWhiteSpace(uploadSetting)
                ? Path.Combine(Directory.GetCurrentDirectory(), "upload")
                : Path.IsPathRooted(uploadSetting)
                    ? uploadSetting
                    : Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), uploadSetting));

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadPath),
                RequestPath = "/storage"
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseMiddleware<JwtTokenMiddleware>();

            app.UseAbpRequestLocalization();
            app.UseHangfireServer();
            app.UseHangfireDashboard("/Project/background/jobs/dashboard");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalRNotificationHub>("/signalr/task-hub");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", $"Project API {_apiVersion}");
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Global.Project.Web.Host.wwwroot.swagger.ui.index.html");
                options.DisplayRequestDuration();
            });
        }
    }
}
