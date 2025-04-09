using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Project.Customer.Authorization;

namespace Project.Customer
{
    [DependsOn(
        typeof(CustomerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CustomerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CustomerAuthorizationProvider>();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CustomerApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
