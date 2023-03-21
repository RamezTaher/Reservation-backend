using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Models.Identity
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }

    public class RoleApiModel
    {
        public List<RoleViewModel> Data { get; set; }
        public bool Success { get; set; }
        public int TotalCount { get; set; }

    }

    public class RolePermissionEntryViewModel
    {
        public int RoleId { get; set; }

        public string PermissionId { get; set; }

        public string PermissionName { get; set; }

        public string Entity { get; set; }

        public string Module { get; set; }

        public bool IsSelected { get; set; }
    }

    public class RoleGroupEntryViewModel
    {
        public int RoleId { get; set; }

        public int GroupId { get; set; }

        public string RoleName { get; set; }

        public string GroupName { get; set; }

        public bool IsSelected { get; set; }
    }

    public class UserPermissionViewModel
    {
        public string PermissionName { get; set; }
    }

}
