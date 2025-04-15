using Abp.Application.Services.Dto;
using Global.Project.Common;
using Global.Project.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Notifications.Dto
{
    public class NotificationDto : EntityDto
    {

        public long? UserId { get; set; }
        public bool? IsRead { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? TaskGroupId { get; set; }
        public int? SourceId { get; set; }
        public string SourceType { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Link { get; set; }

        public TaskStatuses? Status { get; set; }
        public string StatusText { get { return Status.GetValueOrDefault().ToDescription(); } }
    }
}
