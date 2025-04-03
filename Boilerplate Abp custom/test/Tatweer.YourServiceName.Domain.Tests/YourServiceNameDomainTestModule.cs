using Volo.Abp.Modularity;

namespace Tatweer.YourServiceName;

[DependsOn(
    typeof(YourServiceNameDomainModule),
    typeof(YourServiceNameTestBaseModule)
)]
public class YourServiceNameDomainTestModule : AbpModule
{

}
