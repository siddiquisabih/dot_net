using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Tatweer.YourServiceName.MongoDB;

[DependsOn(
    typeof(YourServiceNameApplicationTestModule),
    typeof(YourServiceNameMongoDbModule)
)]
public class YourServiceNameMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
