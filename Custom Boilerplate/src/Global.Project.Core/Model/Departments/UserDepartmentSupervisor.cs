using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace Global.Project.Model.Departments
{
    public class UserDepartmentSupervisor : Entity, IFullAudited
    {
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int UserId { get; set; }
        public int? SupervisorId { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
