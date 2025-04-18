﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Global.Project.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Model
{
    public class UserNotificationGroup : Entity, ICreationAudited, IModificationAudited
    {


        public int NotificationGroupId { get; set; }
        public NotificationGroup NotificationGroup { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
