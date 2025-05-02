using Abp.Application.Services;
using Global.Project.MultiTenancy.Dto;

namespace Global.Project.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

