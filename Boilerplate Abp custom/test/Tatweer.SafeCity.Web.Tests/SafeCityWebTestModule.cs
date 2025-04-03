using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Tatweer.SafeCity.EntityFrameworkCore;
using Tatweer.SafeCity.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Tatweer.SafeCity.Web.Tests
{
    [DependsOn(
        typeof(SafeCityWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class SafeCityWebTestModule : AbpModule
    {
        public SafeCityWebTestModule(SafeCityEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SafeCityWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(SafeCityWebMvcModule).Assembly);
        }
    }
}