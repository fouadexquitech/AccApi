using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AccApi.Data_Layer
{
    public class Mail
    {
        public string SendMail(List<string> MailTo, List<string> MailCC, List<string> MailBCC, string MailSubject, string MailBody, List<string> MailAttach, Boolean BodyHtml, IFormFile AttachFile)
        {
            try
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

                SmtpClient client = new SmtpClient();
                client.Port = Int32.Parse(config["MailSettings:SMTPPort"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = config["MailSettings:SMTPHost"];
                client.Credentials = new System.Net.NetworkCredential(config["MailSettings:SMTPUserName"], config["MailSettings:SMTPPassword"]);

                MailMessage mail = new MailMessage();
                string MailFrom = config["MailSettings:MailFrom"];
                string MailFromName = config["MailSettings:MailFromName"];
                mail.From = new MailAddress(MailFromName + "<" + MailFrom + ">");
                //mail.Headers.Add("Sender", MailFromName);

                foreach (string g in MailTo)
                {
                    MailAddress to = new MailAddress(g);
                    mail.To.Add(to);
                }

                mail.Subject = MailSubject;
                mail.Body = MailBody;
                mail.IsBodyHtml = BodyHtml;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.AlternateViews.Add(Mail_Body(MailBody));

                if (MailCC != null)
                {
                    foreach (string g in MailCC)
                    {
                        MailAddress copy = new MailAddress(g);
                        mail.CC.Add(copy);
                    }
                }

                if (MailBCC != null)
                {
                    foreach (string g in MailBCC)
                    {
                        MailAddress copy = new MailAddress(g);
                        mail.Bcc.Add(copy);
                    }
                }

                if (MailAttach != null)
                {
                    foreach (var attach in MailAttach)
                    {
                        if (File.Exists(attach))
                            mail.Attachments.Add(new Attachment(attach));
                    }
                }

                if (AttachFile != null)
                {
                    string fileName = Path.GetFileName(AttachFile.FileName);
                    if (fileName != "")
                        mail.Attachments.Add(new Attachment(AttachFile.OpenReadStream(), fileName));
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


        private AlternateView Mail_Body(string MailBody)
        {
            string path = Directory.GetCurrentDirectory()+"\\Assets\\Images\\ACC50.jpg";
            LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            Img.ContentId = "MyImage";
            
            string str = @"  
            <table>  
                <tr>  
                    <td> " + MailBody + @"
                    </td>  
                </tr>  
                <tr>  
                    <td>  
                      <img src=cid:MyImage  id='img' alt='' width='100px' height='100px'/>   
                    </td>  
                </tr></table>  
            ";

            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add(Img);
            return AV;
        }

    }
}
