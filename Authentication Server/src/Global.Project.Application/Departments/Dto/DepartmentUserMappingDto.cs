using Abp.Application.Services.Dto;

namespace Global.Project.Departments.Dto
{
    public class DepartmentUserMappingDto : EntityDto
    {
        public DepartmentUserMappingDto()
        {

        }

        public int DepartmentId { get; set; }
        public int? SupervisorId { get; set; }
        public int UserId { get; set; }
    }
}
