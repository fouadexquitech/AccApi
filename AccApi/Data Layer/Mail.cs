using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AccApi.Data_Layer
{
    public class Mail
    {
        public string SendMail(List<General> MailTo, List<General> MailCC, string MailSubject, string MailBody, string MailAttach, Boolean BodyHtml)
        {
            try
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

                SmtpClient client = new SmtpClient();
                client.Port = Int32.Parse(config["SMTPPort"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = config["SMTPHost"];
                client.Credentials = new System.Net.NetworkCredential(config["SMTPUserName"], config["SMTPPassword"]);

                MailMessage mail = new MailMessage();
                string MailFrom = config["MailFrom"];
                string MailFromName = config["MailFromName"];
                mail.From = new MailAddress(MailFromName + "<" + MailFrom + ">");
                //mail.Headers.Add("Sender", MailFromName);

                foreach (General g in MailTo)
                {
                    MailAddress to = new MailAddress(g.mail);
                    mail.To.Add(to);
                }

                mail.Subject = MailSubject;
                mail.Body = MailBody;
                mail.IsBodyHtml = BodyHtml;
                mail.BodyEncoding = System.Text.Encoding.UTF8;

                foreach (General g in MailCC)
                {
                    MailAddress copy = new MailAddress(g.mail);
                    mail.CC.Add(copy);
                }

                if (MailAttach != "")
                {
                    if (File.Exists(MailAttach))
                        mail.Attachments.Add(new Attachment(MailAttach));
                }

                client.Send(mail);
                return "sent";
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return error;
            }
        }


    }
}
