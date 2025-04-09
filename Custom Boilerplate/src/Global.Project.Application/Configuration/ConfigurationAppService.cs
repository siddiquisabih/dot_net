using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Global.Project.Configuration.Dto;

namespace Global.Project.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
