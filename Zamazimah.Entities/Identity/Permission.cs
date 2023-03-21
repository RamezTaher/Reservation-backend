using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Zamazimah.Entities.Identity
{
    public class Permission : IdentityRole
    {
        public Permission(string name)
            : base(name)
        { }

        public Permission()
        {
            this.ApplicationName = "Backend";
        }

        public string ApplicationName { get; set; }
        public string Module { get; set; }
        public string Entity { get; set; }
        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}