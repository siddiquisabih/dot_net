
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace Tatweer.SafeCity.Model.Departments
{
    public class Department : Entity, IFullAudited
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCode { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserDepartmentSupervisor> UserDepartmentSupervisors { get; set; }
    }
}
