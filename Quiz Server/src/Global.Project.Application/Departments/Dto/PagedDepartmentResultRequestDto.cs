using Abp.Application.Services.Dto;

namespace Global.Project.Departments.Dto
{
    public class PagedDepartmentResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
