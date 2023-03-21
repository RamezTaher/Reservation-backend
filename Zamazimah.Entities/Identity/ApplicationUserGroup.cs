using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Entities.Identity
{
    public class ApplicationUserGroup
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
