using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Tatweer.YourServiceName;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(YourServiceNameHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class YourServiceNameConsoleApiClientModule : AbpModule
{

}
