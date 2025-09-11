using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Global.Project.Lookups
{
    public class State : Entity
    {

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

    }
}
