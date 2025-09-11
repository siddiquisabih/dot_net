using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Global.Project.Model.Customers
{
    public class CustomerInformation : FullAuditedEntity
    {

        public CustomerInformation()
        {
            CustomerDocuments = new List<CustomerDocument>();
        }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string PoBox { get; set; }

        public string RegistrationCode { get; set; }

        public int RegistrationTypeId { get; set; }

        public string CompanyName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        public string LinkedinProfile { get; set; }

        public string InstagramProfile { get; set; }

        public int RegionId { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public int TenantId { get; set; }

        public virtual ICollection<CustomerDocument> CustomerDocuments { get; set; }

        public string BusinessType { get; set; }

        public string CompanyLogo { get; set; }

    }
}
