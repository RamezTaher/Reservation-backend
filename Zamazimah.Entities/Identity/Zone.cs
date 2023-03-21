using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zamazimah.Entities.Identity
{
    public class Zone
    {
        public Zone()
        {
            Groups = new List<Group>();
            Users = new List<ApplicationUser>();
        }
        public Zone(string name)
        {
            Groups = new List<Group>();
            Users = new List<ApplicationUser>();
            Name = name;
        }
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Zone Parent { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}