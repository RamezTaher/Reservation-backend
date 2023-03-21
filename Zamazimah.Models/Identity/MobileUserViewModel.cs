using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Models.Identity
{
    public class MobileUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string MobileName { get; set; }
        public string DeviceId { get; set; }
        public bool IsServer { get; set; }
    }
    public class MobileUserFilterViewModel
    {
        public string UserName { get; set; }
        public string MobileName { get; set; }
    }
}
