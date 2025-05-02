using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Global.Project.Authorization;

namespace Global.Project
{
    [DependsOn(
        typeof(ProjectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ProjectModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ProjectAuthorizationProvider>();
        }

        public override void Initialize()
        {

            var thisAssembly = typeof(ProjectModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
