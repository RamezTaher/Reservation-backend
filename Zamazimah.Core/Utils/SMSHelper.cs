using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Core.Utils
{
    public static class SMSHelper
    {
        public static bool SendSMS(string mode, string message, string numbers)
        {
            if(mode != "PROD")
            {
                return false;
            }
            string returnValue = "";
            string url = "";
            url = "https://www.resalaty.com" + "/api/sendsms.php?username=" + "zamazemah" + "&password=" + "5461212" + "&message=" + message + "&numbers=" + numbers + "&sender=" + "alzamazemah" + "&return=string";

            StreamReader strReader;
            WebRequest webReq = WebRequest.Create(url);
            WebResponse webRes = webReq.GetResponse();

            strReader = new StreamReader(webRes.GetResponseStream());

            returnValue = strReader.ReadToEnd();
            if (!string.IsNullOrEmpty(returnValue))
            {
                if (returnValue == "تم استلام الارقام بنجاح")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
