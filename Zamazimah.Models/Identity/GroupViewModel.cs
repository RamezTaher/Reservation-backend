using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Models.Identity
{
    public class GroupViewModel
    {
        //public GroupViewModel()
        //{
        //    Users = new List<UserViewModel>();
        //    //Roles = new List<RoleViewModel>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public int? ZoneId { get; set; }

        //public string ZoneName { get; set; }

        public int UsersCount { get; set; }
        public string ApplicationUserId { get; set; }
        //public List<UserViewModel> Users { get; set; }

        //public List<RoleViewModel> Roles { get; set; }
    }

    public class GroupViewModelList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GroupApiModel
    {
        public List<GroupViewModel> Data { get; set; }
        public bool Success { get; set; }
        public int TotalCount { get; set; }

    }
}
