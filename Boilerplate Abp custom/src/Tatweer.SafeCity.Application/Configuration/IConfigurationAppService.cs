using System.Threading.Tasks;
using Tatweer.SafeCity.Configuration.Dto;

namespace Tatweer.SafeCity.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
