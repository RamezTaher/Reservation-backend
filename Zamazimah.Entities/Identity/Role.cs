using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zamazimah.Entities.Identity
{
    public class Role
    {
        public Role()
        {
            PermissionRoles = new List<PermissionRole>();
            GroupRoles = new List<GroupRole>();
            IsDefault = false;
        }
        public Role(string name)
        {
            PermissionRoles = new List<PermissionRole>();
            GroupRoles = new List<GroupRole>();
            Name = name;
            IsDefault = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public int? ZoneId { get; set; }
        [ForeignKey("ZoneId")]
        public virtual Zone Zone { get; set; }

        public string ZoneName
        {
            get
            {
                if (Zone != null)
                    return Zone.Name;
                else return null;
            }
        }

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
        public virtual ICollection<GroupRole> GroupRoles { get; set; }

        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}