using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Tatweer.SafeCity.Authorization.Users; 

namespace Tatweer.SafeCity.Model.Customer
{
    public class CustomerEntity : Entity 
    {
 

        public string Name { get; set; }

        public string FullName { get; set; }
    }
}
