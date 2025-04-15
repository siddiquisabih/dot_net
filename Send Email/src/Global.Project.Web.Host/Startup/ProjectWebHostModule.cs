﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Global.Project.Configuration;

namespace Global.Project.Web.Host.Startup
{
    [DependsOn(
       typeof(ProjectWebCoreModule))]
    public class ProjectWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ProjectWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ProjectWebHostModule).GetAssembly());

        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            SetUserDepartmentMappingInCache();
        }

        public void SetUserDepartmentMappingInCache()
        {
        }
    }
}
