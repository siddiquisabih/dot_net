using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Global.Project.Lookups.Dto
{
    public class CountryDto : EntityDto
    {

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public string CountryShortCode { get; set; }

        public string PhoneCode { get; set; }

        public string Capital { get; set; }

        public string Currency { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencySymbol { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int RegionId { get; set; }

    }
}
