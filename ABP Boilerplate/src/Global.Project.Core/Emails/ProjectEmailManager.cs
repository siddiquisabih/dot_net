using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Global.Project.Emails
{
    public class ProjectEmailManager : IProjectEmailManager
    {
        public SmtpClient smtp { get; set; }
        public IConfiguration _config { get; set; }
        public string _baseTemplatesPath { get; set; }

        public ProjectEmailManager(IConfiguration config)
        {
            _config = config;
            _baseTemplatesPath = _config.GetValue<string>("Paths:EmailTemplatesPath");
            smtp = new SmtpClient();
            smtp.Host = _config.GetValue<string>("MailSettings:Host");
            smtp.Port = Convert.ToInt32(_config.GetValue<string>("MailSettings:Port"));
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public AlternateView ConfigureTemplate(string body)
        {
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            //LinkedResource admLogo = new LinkedResource(_baseTemplatesPath + "/Images/admLogo.png", "image/png");
            //admLogo.ContentId = "admLogo";
            //admLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            //LinkedResource atsLogo = new LinkedResource(_baseTemplatesPath + "/Images/atsLogo.png", "image/png");
            //atsLogo.ContentId = "atsLogo";
            //atsLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            //LinkedResource emailFooter = new LinkedResource(_baseTemplatesPath + "/Images/emailFooter.png", "image/png");
            //emailFooter.ContentId = "emailFooter";
            //emailFooter.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            //LinkedResource capture = new LinkedResource(_baseTemplatesPath + "/Images/capture.png", "image/png");
            //capture.ContentId = "capture";
            //capture.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            //htmlView.LinkedResources.Add(admLogo);
            //htmlView.LinkedResources.Add(atsLogo);
            //htmlView.LinkedResources.Add(emailFooter);
            //htmlView.LinkedResources.Add(capture);
            return htmlView;
        }

        private MailMessage EmailSetup()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_config.GetValue<string>("MailSettings:Mail"));
            smtp.Credentials = new NetworkCredential(mail.From.Address, _config.GetValue<string>("MailSettings:Password"));
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            return mail;
        }

        //public async Task<bool> SendMail(List<string> email, string body, string subject, AlternateView htmlView)


        public async Task<bool> SendMail(string email, string body, string subject, AlternateView alternateView)
        {
            try
            {
                var mail = EmailSetup();
                mail.AlternateViews.Add(alternateView);
                foreach (var toEmail in email) { mail.To.Add(new MailAddress(email)); }
                mail.IsBodyHtml = true;
                mail.Body = body;
                mail.Subject = subject;
                await smtp.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                string message = "";
                if (email != null)
                {
                    message = $"email To : {string.Join(",", email)}--";
                }
                if (subject != null)
                {
                    message += $"subject : {subject}";
                }

                throw new Exception(message, ex);
            }
        }

        public async Task<bool> SendAccountVerificationEmail(string email, string userName, string companyName)
        {

            {

                string templatePath = Path.Combine(_baseTemplatesPath, "CustomerOnboarding.htm");
                var body = await File.ReadAllTextAsync(templatePath);

                body = body.Replace("#UserName#", userName);
                body = body.Replace("#UserEmail#", email);
                body = body.Replace("#CompanyName#", companyName);

                var alternateView = ConfigureTemplate(body);
                var isSuccess = await SendMail(email, body, "Onboarding", alternateView);
                return isSuccess;

            }
        }

        public async Task<bool> SendLoginOtpEmail(string email, string otpCode)
        {
            {
                string lang = CultureInfo.CurrentCulture.Name;
                string fileName = lang == "en" ? "LoginOTP.htm" :
                    lang == "ar" ? "LoginOTP-ar.htm" :
                        lang == "ur" ? "LoginOTP-ur.htm" :
                            "LoginOTP.htm";

                string templatePath = Path.Combine(_baseTemplatesPath, fileName);
                var body = await File.ReadAllTextAsync(templatePath);
                body = body.Replace("#OTPCODE#", otpCode);
                var alternateView = ConfigureTemplate(body);
                var isSuccess = await SendMail(email, body, "Verification", alternateView);
                return isSuccess;

            }
        }

        public async Task<bool> SendManualEmailToCustomer(string ToEmail, string userEmail, string userName, string userCompanyName)
        {
            {

                string templatePath = Path.Combine(_baseTemplatesPath, "CustomerOnboarding.htm");
                var body = await File.ReadAllTextAsync(templatePath);

                body = body.Replace("#UserName#", userName);
                body = body.Replace("#UserEmail#", userEmail);
                body = body.Replace("#CompanyName#", userCompanyName);

                var alternateView = ConfigureTemplate(body);
                var isSuccess = await SendMail(ToEmail, body, "Verification", alternateView);
                return isSuccess;

            }
        }

    }
}
