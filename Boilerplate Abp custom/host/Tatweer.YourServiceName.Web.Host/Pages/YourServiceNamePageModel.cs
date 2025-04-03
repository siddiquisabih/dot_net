using Tatweer.YourServiceName.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tatweer.YourServiceName.Pages;

public abstract class YourServiceNamePageModel : AbpPageModel
{
    protected YourServiceNamePageModel()
    {
        LocalizationResourceType = typeof(YourServiceNameResource);
    }
}
