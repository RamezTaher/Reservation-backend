using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Models.Identity
{
    public class GroupUserViewModel
    {
        public int GroupId { get; set; }
        public string ApplicationUserId { get; set; }
        public bool AddRemove { get; set;}
    }

    public class GroupUserApiModel
    {
        public List<GroupUserViewModel> Data { get; set; }
        public bool Success { get; set; }
        public int TotalCount { get; set; }

    }
}
