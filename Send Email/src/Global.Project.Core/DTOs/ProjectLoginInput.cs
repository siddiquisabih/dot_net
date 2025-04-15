using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Global.Project.DTOs
{
    public class ProjectLoginInput
    {
        public string login { get; set; }
        public string password { get; set; }
    }
}
