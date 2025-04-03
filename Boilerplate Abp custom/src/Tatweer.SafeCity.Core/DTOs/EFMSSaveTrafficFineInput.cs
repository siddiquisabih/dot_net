using System;
using System.Collections.Generic;
using System.Text;

namespace Tatweer.SafeCity.DTOs
{
    public class EFMSSaveTrafficFineInput
    {
        public string plate_no { get; set; }

        public string location_name { get; set; }
        public string violations { get; set; }
        public int? speed { get; set; }

        public DateTime issued_at { get; set; }
    }
}
