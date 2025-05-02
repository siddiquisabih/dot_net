using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Users.Dto
{
    public class ProjectAttachmentDto : EntityDto
    {
        public int EntityId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public byte[] ImageInBytes { get; set; }
        public string OriginFileName { get; set; }
        public string AttachmentType { get; set; }
        public string FileType { get; set; }
    }
}
