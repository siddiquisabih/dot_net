using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Global.Project.Model.Customers;

namespace Global.Project.Customers.Dto
{
    public class CreateCustomerInformationDto
    {
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

        public List<CreateCustomerDocumentDto> CustomerDocuments { get; set; }

        public string BusinessType { get; set; }

        public string CompanyLogo { get; set; }


    }
}
