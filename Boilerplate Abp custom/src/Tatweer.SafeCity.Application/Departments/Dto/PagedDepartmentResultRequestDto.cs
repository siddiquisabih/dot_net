using Abp.Application.Services.Dto;

namespace Tatweer.SafeCity.Departments.Dto
{
    //custom PagedResultRequestDto
    public class PagedDepartmentResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
