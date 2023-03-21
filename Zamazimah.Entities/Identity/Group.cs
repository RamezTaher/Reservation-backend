using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zamazimah.Entities.Identity
{
    public class Group
    {
        public Group()
        {
            GroupRoles = new List<GroupRole>();
            ApplicationUserGroups = new List<ApplicationUserGroup>();
            IsDefault = false;
        }
        public Group(string name)
        {
            GroupRoles = new List<GroupRole>();
            ApplicationUserGroups = new List<ApplicationUserGroup>();
            Name = name;
            IsDefault = false;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ZoneId { get; set; }
        public bool IsDefault { get; set; }
        [ForeignKey("ZoneId")]
        public virtual Zone Zone { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public virtual ICollection<GroupRole> GroupRoles { get; set; }
        [NotMapped]
        public string ZoneName
        {
            get
            {
                if (Zone != null)
                    return Zone.Name;
                else return null;
            }
            set { }
        }
    }
}