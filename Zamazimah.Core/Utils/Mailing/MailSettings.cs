using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Core.Utils.Mailing
{
    public class MailSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Sender { get; set; }
    }
}
