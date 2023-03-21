using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Models.Identity
{
    public class PermissionViewModel
    {
        public string Id { get; set; }

        public string ApplicationName { get; set; }
        public string Name { get; set; }

        public string Module { get; set; }

        public string Entity { get; set; }
        public int RoleId { get; set; }

        public bool Selected { get; set; }
    }

    public class PermissionApiModel
    {
        public List<PermissionViewModel> Data { get; set; }
        public bool Success { get; set; }
        public int TotalCount { get; set; }

    }
}
