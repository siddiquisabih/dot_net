using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Users.Dto
{
    public class PasswordChangeRequestDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
