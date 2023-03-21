using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Models.Identity
{
    public class GroupRoleViewModel
    {
        public int RoleId { get; set; }
    }

    public class GroupRoleApiModel
    {
        public List<GroupRoleViewModel> Data { get; set; }
        public bool Success { get; set; }
        public int TotalCount { get; set; }

    }
}
