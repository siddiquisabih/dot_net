using System.ComponentModel.DataAnnotations;

namespace Tatweer.SafeCity.Customer.Dto
{
    public class CreateCustomerDto
    {
         
        [Required]
        public string Name { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
