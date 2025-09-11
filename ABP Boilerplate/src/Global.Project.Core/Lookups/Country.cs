using System.ComponentModel.DataAnnotations.Schema;

using Abp.Domain.Entities;

namespace Global.Project.Lookups
{
    public class Country : Entity
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }
        
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

        [ForeignKey(nameof(RegionId))]
        public Region Region { get; set; }

    }
}
