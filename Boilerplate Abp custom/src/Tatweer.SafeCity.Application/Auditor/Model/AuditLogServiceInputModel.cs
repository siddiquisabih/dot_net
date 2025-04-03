using System;

namespace Tatweer.SafeCity.Auditor.Model
{
    public class AuditLogServiceInputModel
    {
        public int UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? TypeId { get; set; }
    }
}
