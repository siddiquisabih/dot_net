using Abp.Application.Services.Dto;
using System;

namespace Tatweer.SafeCity.Auditor.Model
{
    public class AuditLogModel : EntityDto
    {
        public AuditLogModel()
        {

        }

        public string AuditType { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
