using Tatweer.YourServiceName.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Tatweer.YourServiceName.Blazor.Server.Host;

public abstract class YourServiceNameComponentBase : AbpComponentBase
{
    protected YourServiceNameComponentBase()
    {
        LocalizationResource = typeof(YourServiceNameResource);
    }
}
