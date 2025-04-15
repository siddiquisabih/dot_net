using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Services;
using Abp.Net.Mail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Global.Project.SendEmail
{
    public class SendEmailAppService : ApplicationService
    {
 
        public async Task<string> SendEmail(string email)
        {
            using (var client = new SmtpClient())
            {
              
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("example@gmail.com", "app-password");
               
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("from email here"),
                    Subject = "Test Email",
                    Body = "This is just to bring to your kind attention that this email was sent from the .NET server.",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);


                return "Email Sent";
            }
        }
    }
}
