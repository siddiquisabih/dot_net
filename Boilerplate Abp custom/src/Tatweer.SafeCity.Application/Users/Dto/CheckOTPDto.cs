using System;
using System.Collections.Generic;
using System.Text;

namespace Tatweer.SafeCity.Users.Dto
{
    public class CheckOTPDto
    {
        public string EmailAddress { get; set; }
        public string OTPCode { get; set; }
    }
}
