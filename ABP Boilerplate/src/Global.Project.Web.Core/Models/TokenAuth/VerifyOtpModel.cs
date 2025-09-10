using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Project.Models.TokenAuth
{
    public class VerifyOtpModel
    {
        public string EmailAddress { get; set; }
        public string OTP { get; set; }
    }
}
