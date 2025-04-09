using System.Threading.Tasks;
using Global.Project.Configuration.Dto;

namespace Global.Project.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
