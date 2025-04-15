using Abp.Application.Services;
using Microsoft.Extensions.Configuration;
using Project.Customer.Enums;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.Customer.Emails
{
    public interface ICustomerEmailManager:IApplicationService
    {
        public SmtpClient smtp { get; set; }
        public IConfiguration _config { get; set; }
        public string _baseTemplatesPath { get; set; }
        Task<bool> SendMailTicketRequestReminderTemplate(List<string> email, string name, string subject, string title, string date, string requestNo, string technicianName, string technicianEmail, string autoCloseDate, ReminderTypes status, string comment, string additionalMsg, string requestLink);
    }
}
