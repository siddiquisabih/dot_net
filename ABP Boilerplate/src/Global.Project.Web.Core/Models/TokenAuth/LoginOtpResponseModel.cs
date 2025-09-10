using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Project.Models.TokenAuth
{
    public class LoginOtpResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public string TempOtp { get; set; }



    }
}
