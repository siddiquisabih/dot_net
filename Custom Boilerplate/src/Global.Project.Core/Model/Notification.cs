using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Global.Project.Authorization.Users;
using Global.Project.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Model
{
    public class Notification : Entity, ICreationAudited, IModificationAudited, IDeletionAudited
    {
        public long? UserId { get; set; }
        public User User{ get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? SourceId { get; set; }
        public string SourceType { get; set; }
        public long? CreatorUserId { get ; set ; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string Link { get; set; }

        public int? TaskGroupId { get; set; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get; set ; }
        public bool IsDeleted { get ; set; }
        public TaskStatuses? Status { get; set; }
    }
}
