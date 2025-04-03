using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tatweer.SafeCity.DTOs
{
    public class RNPLoginInput
    {
        public string login { get; set; }
        public string password { get; set; }
    }
}
