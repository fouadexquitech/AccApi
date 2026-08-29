using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class MailForSending
    {
        public List<string> To { get; set; } = new();
        public List<string> Cc { get; set; } = new();
        public List<string> Bcc { get; set; } = new();
        public List<string> Attachments { get; set; } = new();

        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; } = true;
    }
}
