using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.DTOs
{
    public class NotificationGroupDto:EntityDto
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        public List<long> AssigneeIds { get; set; }
    }
}
