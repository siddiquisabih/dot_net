using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Tatweer.SafeCity.Configuration;
using Tatweer.SafeCity.Common;

namespace Tatweer.SafeCity.Web.Host.Startup
{
    [DependsOn(
       typeof(SafeCityWebCoreModule))]
    public class SafeCityWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SafeCityWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SafeCityWebHostModule).GetAssembly());
            //var thisAssembly = typeof(SafeCityWebHostModule).GetAssembly();
            //Configuration.Modules.AbpAutoMapper().Configurators.Add(
            //    // Scan the assembly for classes which inherit from AutoMapper.Profile
            //    cfg => cfg.AddMaps(thisAssembly)
            //);
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            SetUserDepartmentMappingInCache();
        }

        public void SetUserDepartmentMappingInCache()
        {
            //var dataService = IocManager.Resolve<IDataService>();
            //dataService.RefreshMapping();
           
        }
    }
}
