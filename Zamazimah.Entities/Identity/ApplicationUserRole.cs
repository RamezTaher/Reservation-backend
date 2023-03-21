using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Entities.Identity
{
    public class ApplicationUserRole
    {
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
