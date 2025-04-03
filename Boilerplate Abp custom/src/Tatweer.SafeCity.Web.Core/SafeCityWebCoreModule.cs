using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Tatweer.ITSM;
using Tatweer.SafeCity.Authentication.JwtBearer;
using Tatweer.SafeCity.Configuration;
using Tatweer.SafeCity.EntityFrameworkCore;


namespace Tatweer.SafeCity
{
    [DependsOn(
		  typeof(SafeCityApplicationModule)
		 ,typeof(SafeCityEntityFrameworkModule)
		 ,typeof(AbpAspNetCoreModule)
		 ,typeof(AbpAspNetCoreSignalRModule)

		 ,typeof(ITSMWebCoreModule)
		 ,typeof(SafeCityApplicationModule)
     )]
	public class SafeCityWebCoreModule : AbpModule
	{
		private readonly IWebHostEnvironment _env;
		private readonly IConfigurationRoot _appConfiguration;

		public SafeCityWebCoreModule(IWebHostEnvironment env)
		{
			_env = env;
			_appConfiguration = env.GetAppConfiguration();
		}

		public override void PreInitialize()
		{
			Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
				SafeCityConsts.ConnectionStringName
			);

			// Use database for language management
			Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

			Configuration.Modules.AbpAspNetCore()
				 .CreateControllersForAppServices(
					 typeof(SafeCityApplicationModule).GetAssembly()
				 );

			ConfigureTokenAuth();
			Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;

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
			IocManager.RegisterAssemblyByConvention(typeof(SafeCityWebCoreModule).GetAssembly());
		}

		public override void PostInitialize()
		{
			IocManager.Resolve<ApplicationPartManager>()
				.AddApplicationPartsIfNotAddedBefore(typeof(SafeCityWebCoreModule).Assembly);
		}
	}
}
