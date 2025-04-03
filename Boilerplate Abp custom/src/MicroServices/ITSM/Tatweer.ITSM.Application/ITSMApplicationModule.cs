using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Tatweer.ITSM;
using Tatweer.ITSM.Authorization;


namespace Tatweer.ITSM
{
    [DependsOn(
        typeof(ITSMCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ITSMApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ITSMAuthorizationProvider>();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ITSMApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
            //IocManager.RegisterAssemblyByConvention(typeof(IUserLocationBusinessAppService).GetAssembly());
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
