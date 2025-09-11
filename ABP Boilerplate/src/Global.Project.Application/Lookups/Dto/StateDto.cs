using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Global.Project.Lookups.Dto
{
    public class StateDto : EntityDto
    {

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int CountryId { get; set; }

    }
}
