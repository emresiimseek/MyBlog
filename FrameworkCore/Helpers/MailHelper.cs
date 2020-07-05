using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.Helpers
{
    public class MailHelper
    {
        public static bool SendMail(string body, string to, string subject, bool isHtml = true)
        {

            return SendMail(body, new List<string> { to }, subject, isHtml);
            //WebMail.SmtpServer = "smtp.gmail.com";
            //WebMail.SmtpPort = 587;
            //WebMail.UserName = "emresimseka@gmail.com";
            //WebMail.Password = "dYorK5sr";
            //WebMail.EnableSsl = true;

            //WebMail.Send(
            //            to: to, subject: subject,
            //            body: body);

            //return true;
        }

        public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        {
            bool result = false;
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(ConfigHelper.GetConfigPar<string>("MailUser"));
                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                using (var smtp = new SmtpClient(ConfigHelper.GetConfigPar<string>("MailHost"), ConfigHelper.GetConfigPar<int>("MailPort")))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(ConfigHelper.GetConfigPar<string>("MailUser"), ConfigHelper.GetConfigPar<string>("MailPass"));
                    smtp.Send(message);
                    result = true;
                }
            }
            catch (Exception)
            {

            }
            return result;
        }
    }
}
