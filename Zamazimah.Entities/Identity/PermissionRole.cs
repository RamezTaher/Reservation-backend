using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Entities.Identity
{
    public class PermissionRole
    {
        public string PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
