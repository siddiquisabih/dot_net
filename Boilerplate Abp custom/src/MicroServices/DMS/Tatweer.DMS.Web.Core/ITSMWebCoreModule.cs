using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Tatweer.ITSM.Authentication.JwtBearer;
using Tatweer.ITSM.Configuration;
using Tatweer.ITSM.EntityFrameworkCore;

namespace Tatweer.ITSM
{
	[DependsOn(
         typeof(ITSMApplicationModule),
         typeof(ITSMEntityFrameworkModule),
         typeof(AbpAspNetCoreModule)
        ,typeof(AbpAspNetCoreSignalRModule)
     )]
    public class ITSMWebCoreModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRecurringJobManager _backgroundJob;
        public ITSMWebCoreModule(IWebHostEnvironment env, IRecurringJobManager backgroundJob)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
            _backgroundJob = backgroundJob;
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                ITSMConsts.ConnectionStringName
            );

            // Use database for language management
           //// Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(ITSMApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ITSMWebCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ITSMWebCoreModule).Assembly);

            //Configuration.Modules.AbpAspNetCore()
            //  .CreateControllersForAppServices(
            //      typeof(SmartOfficerApplicationModule).GetAssembly(),
            //      moduleName: "SmartOfficer"
            //  );

        }
    }
}
