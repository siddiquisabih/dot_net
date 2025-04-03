using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Tatweer.SafeCity.Authorization;

namespace Tatweer.SafeCity
{
    [DependsOn(
        typeof(SafeCityCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SafeCityApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SafeCityAuthorizationProvider>();
        }

        public override void Initialize()
        {

            var thisAssembly = typeof(SafeCityApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
