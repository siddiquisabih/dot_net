using System;
using System.Collections.Generic;
using System.Text;

namespace Tatweer.SafeCity.Common.Emails
{
    public class EmailRequest
    {
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public List<string> ToEmail { get; set; }
        public List<string> CCEmail { get; set; }
        public string NotificationType { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string ChangeDate { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianEmail { get; set; }
        public string RequestOldStatus { get; set; }
        public string RequestNewStatus { get; set; }
        public string RequestOldOwner { get; set; }
        public string RequestNewOwner { get; set; }
        public string AccountName { get; set; }
        public string Comment { get; set; }
        public string RequestLink { get; set; }
    }
}
