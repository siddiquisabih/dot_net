using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Global.Project.Model.Customers
{
    public class CustomerDocument : FullAuditedEntity
    {
        public string OriginalFileName { get; set; }

        public string SavedFileName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int CustomerInformationId { get; set; }

        [ForeignKey(nameof(CustomerInformationId))]

        public bool IsExpired
        {
            get
            {
                return ExpiryDate.HasValue && ExpiryDate.Value < DateTime.UtcNow;
            }
        }

        public long FileSize { get; set; }

    }
}
