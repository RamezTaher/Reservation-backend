using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Entities.Identity
{
    public class GroupRole
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
