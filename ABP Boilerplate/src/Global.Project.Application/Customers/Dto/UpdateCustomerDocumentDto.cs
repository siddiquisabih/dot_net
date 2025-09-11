using System;
using Abp.Application.Services.Dto;

namespace Global.Project.Customers.Dto
{
    public class UpdateCustomerDocumentDto : EntityDto
    {
        public string OriginalFileName { get; set; }

        public string SavedFileName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public long FileSize { get; set; }
    }
}
