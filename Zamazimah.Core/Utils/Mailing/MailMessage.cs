using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Core.Utils.Mailing
{
    public class MessageModel
    {
        public MessageModel()
        {
            BCC = new List<string>() { };
            To = new List<string>() { };
        }

        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Attachments { get; set; }
    }
}
