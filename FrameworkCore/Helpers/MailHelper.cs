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
        }

        public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        {

            bool result = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Timeout = 600000;
            smtp.Host = ConfigHelper.GetConfigPar<string>("MailHost");
            smtp.Port = ConfigHelper.GetConfigPar<int>("MailPort");
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(ConfigHelper.GetConfigPar<string>("MailUser"), ConfigHelper.GetConfigPar<string>("MailPass"));

            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(ConfigHelper.GetConfigPar<string>("MailUser"));
        
            to.ForEach(x => { mailMsg.To.Add(new MailAddress(x)); });
            mailMsg.Subject = subject;
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = isHtml;
            mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            try
            {
                smtp.Send(mailMsg);
                result = true;
            }
            catch (Exception)
            {
                throw new Exception();
            }


            
            return result;
        }

        public static void SendMail2(string to, string subject, string body, bool isHtml = true)
        {

            //bool result = false;
            //try
            //{
            //    var message = new MailMessage();
            //    message.From = new MailAddress(ConfigHelper.GetConfigPar<string>("MailUser"));
            //    to.ForEach(x =>
            //    {
            //        message.To.Add(new MailAddress(x));
            //    });
            //    message.Subject = subject;
            //    message.Body = body;
            //    message.IsBodyHtml = isHtml;
            //    using (var smtp = new SmtpClient(ConfigHelper.GetConfigPar<string>("MailHost"), ConfigHelper.GetConfigPar<int>("MailPort")))
            //    {

            //        smtp.EnableSsl = true;
            //        smtp.Credentials = new NetworkCredential(ConfigHelper.GetConfigPar<string>("MailUser"), ConfigHelper.GetConfigPar<string>("MailPass"));
            //        smtp.Send(message);
            //        result = true;
            //    }
            //}
            //catch (Exception e)
            //{
            //    if (e.Message!=null)
            //    {
            //        throw;
            //    }
            //}

            //SmtpClient smtp = new SmtpClient();
            //smtp.Timeout = 600000;
            //smtp.Host = ConfigHelper.GetConfigPar<string>("MailHost");
            //smtp.Port = ConfigHelper.GetConfigPar<int>("MailPort");
            //smtp.EnableSsl = false;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = new System.Net.NetworkCredential(ConfigHelper.GetConfigPar<string>("MailUser"), ConfigHelper.GetConfigPar<string>("MailPass"));

            //MailMessage mailMsg = new MailMessage();
            //mailMsg.From = new MailAddress(ConfigHelper.GetConfigPar<string>("MailUser"));
            //mailMsg.To.Add(new MailAddress(to));
            //mailMsg.Subject = subject;
            //mailMsg.Body = body;
            //mailMsg.IsBodyHtml = isHtml;
            //mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //smtp.Send(mailMsg);


        }
    }
}
