using Abp.Application.Services.Dto;

namespace Global.Project.Users.Dto
{
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
