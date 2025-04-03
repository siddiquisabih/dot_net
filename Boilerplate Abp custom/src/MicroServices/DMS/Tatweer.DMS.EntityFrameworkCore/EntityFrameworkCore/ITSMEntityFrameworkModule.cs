using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tatweer.ITSM.Configuration;
using Tatweer.ITSM.EntityFrameworkCore.Seed;

namespace Tatweer.ITSM.EntityFrameworkCore
{
	[DependsOn(
        typeof(ITSMCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class ITSMEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<ITSMDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        ITSMDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        IHostingEnvironment env = IocManager.Resolve<IHostingEnvironment>();
                        var connString = env.GetAppConfiguration().GetConnectionString(ITSMConsts.ConnectionStringName);

                        ITSMDbContextConfigurer.Configure(options.DbContextOptions, connString);
                    }

                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ITSMEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
