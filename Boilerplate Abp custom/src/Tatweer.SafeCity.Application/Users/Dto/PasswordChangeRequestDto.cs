using System;
using System.Collections.Generic;
using System.Text;

namespace Tatweer.SafeCity.Users.Dto
{
    public class PasswordChangeRequestDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
