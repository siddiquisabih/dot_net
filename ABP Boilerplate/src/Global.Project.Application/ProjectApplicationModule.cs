using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Global.Project.Authorization;
using Global.Project.Features;

namespace Global.Project
{
    [DependsOn(
        typeof(ProjectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ProjectAuthorizationProvider>();
            Configuration.Features.Providers.Add<ProjectFeatureProvider>();
        }

        public override void Initialize()
        {

            var thisAssembly = typeof(ProjectApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
