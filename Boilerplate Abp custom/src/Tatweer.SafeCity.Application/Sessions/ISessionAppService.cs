using System.Threading.Tasks;
using Abp.Application.Services;
using Tatweer.SafeCity.Sessions.Dto;

namespace Tatweer.SafeCity.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
