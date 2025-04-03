using System.Threading.Tasks;
using Abp.Application.Services;
using Tatweer.SafeCity.Authorization.Accounts.Dto;

namespace Tatweer.SafeCity.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
