using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Tatweer.SafeCity.MultiTenancy;

namespace Tatweer.SafeCity.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
