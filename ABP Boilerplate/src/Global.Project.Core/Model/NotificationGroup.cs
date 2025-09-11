using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Global.Project.Model
{
    public class NotificationGroup : Entity, ICreationAudited, IModificationAudited, IDeletionAudited
    {
        public NotificationGroup()
        {
            UserNotificationGroups = new List<UserNotificationGroup>();
        }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public virtual ICollection<UserNotificationGroup> UserNotificationGroups { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
