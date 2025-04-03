
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Tatweer.ITSM.Enums;
using Tatweer.ITSM;

namespace Tatweer.ITSM.Emails
{
    public class ITSMEmailManager :   ITSMIEmailManager
    {
        public SmtpClient smtp { get; set; }
        public IConfiguration _config { get; set; }
        public string _baseTemplatesPath { get; set; }

        public ITSMEmailManager(IConfiguration config)
        {
            _config = config;
            _baseTemplatesPath = _config.GetValue<string>("Paths:TemplatesPath");
            smtp = new SmtpClient();
            smtp.Port = Convert.ToInt32(_config.GetValue<string>("MailSettings:Port"));
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = _config.GetValue<string>("MailSettings:Host");
        }


        public async Task<bool> SendMailTicketRequestReminderTemplate(List<string> email, string name, string subject, string title, string date, string requestNo, string technicianName, string technicianEmail, string autoCloseDate, ReminderTypes status, string comment, string additionalMsg, string requestLink)
        {
            bool isSuccess = false;
            if (email != null && email.Count <= 0)
            {
                return isSuccess;
            }
            var hedaerText = (status == ReminderTypes.RTR || status == ReminderTypes.RTFR) ? "Please note that your below request is updated as resolved, and waiting your confirmation:" :
                ((status == ReminderTypes.WFCTR || status == ReminderTypes.WFCTFR) ? "Please note that the below request is waiting your action:" :
                "Please note that the below request is set On-Hold, and waiting your action:");

            var footerText = (status == ReminderTypes.RTR || status == ReminderTypes.RTFR) ? "Please let us know if the above request is resolved, so we can update our records accordingly." :
               ((status == ReminderTypes.WFCTR || status == ReminderTypes.WFCTFR) ? "The request will be automatically closed by " + autoCloseDate + " if no response is received from your side before that date." :
               "The request will be automatically closed by " + autoCloseDate + " if remained On-Hold until that date.");

            var body = System.IO.File.ReadAllText(_baseTemplatesPath + "/TicketRequestReminderTemplate.html");
            body = body.Replace("#Name#", name);
            body = body.Replace("#TicketNo#", requestNo);
            body = body.Replace("#RequestDate#", date);
            body = body.Replace("#Title#", title);
            body = body.Replace("#TechnicianName#", technicianName);
            body = body.Replace("#TechnicianEmail#", technicianEmail);
            body = body.Replace("#RequestLink#", requestLink);
            body = body.Replace("#HeaderMsg#", hedaerText);
            body = body.Replace("#FooterMsg#", footerText);
            body = body.Replace("#Comment#", comment);
            body = body.Replace("#AdditionalMsg#", string.IsNullOrEmpty(additionalMsg) ? "" : "<p style='font - family: monospace; font - weight:normal; '>" + additionalMsg + "</p>");

            var alternateView = ConfigureTemplate(body);
            isSuccess = await SendMail(email, body, subject, alternateView);
            return isSuccess;
        }

        public AlternateView ConfigureTemplate(string body)
        {
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            LinkedResource admLogo = new LinkedResource(_baseTemplatesPath + "/Images/admLogo.png", "image/png");
            admLogo.ContentId = "admLogo";
            admLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            LinkedResource atsLogo = new LinkedResource(_baseTemplatesPath + "/Images/atsLogo.png", "image/png");
            atsLogo.ContentId = "atsLogo";
            atsLogo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            LinkedResource emailFooter = new LinkedResource(_baseTemplatesPath + "/Images/emailFooter.png", "image/png");
            emailFooter.ContentId = "emailFooter";
            emailFooter.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            LinkedResource capture = new LinkedResource(_baseTemplatesPath + "/Images/capture.png", "image/png");
            capture.ContentId = "capture";
            capture.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            htmlView.LinkedResources.Add(admLogo);
            htmlView.LinkedResources.Add(atsLogo);
            htmlView.LinkedResources.Add(emailFooter);
            htmlView.LinkedResources.Add(capture);
            return htmlView;
        }
        public async Task<bool> SendMail(List<string> email, string body, string subject, AlternateView htmlView)
        {
            try
            {
                MailMessage mail = EmailSetup();
                mail.AlternateViews.Add(htmlView);
                foreach (var toEmail in email) { mail.To.Add(new MailAddress(toEmail)); }
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

        private MailMessage EmailSetup()
        {
            // set email
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress(_config.GetValue<string>("MailSettings:Mail"));
            smtp.Credentials = new NetworkCredential(mail.From.Address, _config.GetValue<string>("MailSettings:Password"));
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;

            return mail;
        }
    }
}
