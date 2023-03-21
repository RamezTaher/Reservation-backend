using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Models.Generic
{
    public class MailModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
    }
}
