using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace Global.Project.Model
{
    public class AuditType : Entity, IFullAudited
    {
        public string AuditTypeCode { get; set; }
        public string AuditTypeName { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
}
