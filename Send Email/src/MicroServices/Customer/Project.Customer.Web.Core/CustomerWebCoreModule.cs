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
using Project.Customer;
using Project.Customer.Configuration;
using Project.Customer.Authentication.JwtBearer;
using Project.Customer.EntityFrameworkCore;
using System;
using System.Text;

namespace Project.Customer
{
	[DependsOn(
         typeof(CustomerApplicationModule),
         typeof(CustomerEntityFrameworkModule),
         typeof(AbpAspNetCoreModule)
        ,typeof(AbpAspNetCoreSignalRModule)
     )]
    public class CustomerWebCoreModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRecurringJobManager _backgroundJob;
        public CustomerWebCoreModule(IWebHostEnvironment env, IRecurringJobManager backgroundJob)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
            _backgroundJob = backgroundJob;
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                CustomerConsts.ConnectionStringName
            );


            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(CustomerApplicationModule).GetAssembly()
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
            IocManager.RegisterAssemblyByConvention(typeof(CustomerWebCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(CustomerWebCoreModule).Assembly);

        }
    }
}
