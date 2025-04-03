using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Tatweer.YourServiceName.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class YourServiceNameBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "YourServiceName";
}
