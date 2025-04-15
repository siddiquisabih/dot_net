using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Model
{
    public class ProjectAttachment : FullAuditedEntity
    {
        public int EntityId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentType { get; set; }
        public string FileType { get; set; }
    }
}
