using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

// this is entity class hai




namespace Tatweer.SafeCity.Customers
{
    public class Customer : FullAuditedEntity<Guid>, ICreationAudited, IModificationAudited
    {
        public string Name { get; set; } = string.Empty;


        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }

}