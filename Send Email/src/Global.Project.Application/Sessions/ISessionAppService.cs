using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.Sessions.Dto;

namespace Global.Project.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
