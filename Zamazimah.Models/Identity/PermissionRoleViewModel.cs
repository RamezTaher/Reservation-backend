using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Models.Identity
{
    public class PermissionRoleViewModel
    {
        public string PermissionId { get; set; }
    }

    public class PermissionRoleApiModel
    {
        public List<PermissionRoleViewModel> Data { get; set; }
        public bool Success { get; set; }
        public int TotalCount { get; set; }

    }


    public class PermissionRoleModel
    {
        public string PermissionId { get; set; }
        public string PermissionName { get; set; }
        public bool IsChecked { get; set; }
        public string PermissionEntity { get; set; }
    }
    public class PermissionEntityModel
    {
        public string PermissionEntity { get; set; }
        public bool IsChecked { get; set; }
        public List<PermissionRoleModel> Permissions { get; set; }
    }


    public class PermissioCheckedModel
    {
        public string PermissionId { get; set; }
        public bool IsChecked { get; set; }
    }
}
