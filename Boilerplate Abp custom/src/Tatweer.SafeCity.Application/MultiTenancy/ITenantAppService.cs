using Abp.Application.Services;
using Tatweer.SafeCity.MultiTenancy.Dto;

namespace Tatweer.SafeCity.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

