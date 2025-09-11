using Abp.Application.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Global.Project.Emails
{
    public interface IProjectEmailManager : IApplicationService
    {
        public SmtpClient smtp { get; set; }

        public IConfiguration _config { get; set; }

        public string _baseTemplatesPath { get; set; }

        public Task<bool> SendAccountVerificationEmail(string email, string userName, string companyName);

        public Task<bool> SendManualEmailToCustomer(string ToEmail, string userEmail, string userName, string userCompanyName);
     
        public Task<bool> SendLoginOtpEmail(string email, string otpCode);
    }
}
