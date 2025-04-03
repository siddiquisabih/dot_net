using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Tatweer.YourServiceName;

[Dependency(ReplaceServices = true)]
public class YourServiceNameBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "YourServiceName";
}
