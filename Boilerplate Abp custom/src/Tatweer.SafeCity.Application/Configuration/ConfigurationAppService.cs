﻿using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Tatweer.SafeCity.Configuration.Dto;

namespace Tatweer.SafeCity.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SafeCityAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
