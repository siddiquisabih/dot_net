using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Project.Customer.Configuration;
using Project.Customer.EntityFrameworkCore.Seed;

namespace Project.Customer.EntityFrameworkCore
{
	[DependsOn(
        typeof(CustomerCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class CustomerEntityFrameworkModule : AbpModule
    {
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<CustomerDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        CustomerDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        var env = IocManager.Resolve<IHostingEnvironment>();
                        var connString = env.GetAppConfiguration().GetConnectionString(CustomerConsts.ConnectionStringName);

                        CustomerDbContextConfigurer.Configure(options.DbContextOptions, connString);
                    }

                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CustomerEntityFrameworkModule).GetAssembly());
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
