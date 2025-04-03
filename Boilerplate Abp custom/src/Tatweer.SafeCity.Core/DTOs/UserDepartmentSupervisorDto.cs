using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace Tatweer.SafeCity
{
    public class UserDepartmentSupervisorDto : EntityDto
    {
        public int DepartmentId { get; set; }
        public int? SupervisorId { get; set; }
        public int UserId { get; set; }
    }
}
