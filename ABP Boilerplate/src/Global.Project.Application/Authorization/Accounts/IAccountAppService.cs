using System.Threading.Tasks;
using Abp.Application.Services;
using Global.Project.Authorization.Accounts.Dto;

namespace Global.Project.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
