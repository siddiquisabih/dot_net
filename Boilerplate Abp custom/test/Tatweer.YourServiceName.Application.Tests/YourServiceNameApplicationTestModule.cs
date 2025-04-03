using Volo.Abp.Modularity;

namespace Tatweer.YourServiceName;

[DependsOn(
    typeof(YourServiceNameApplicationModule),
    typeof(YourServiceNameDomainTestModule)
    )]
public class YourServiceNameApplicationTestModule : AbpModule
{

}
