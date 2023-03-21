using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.Identity
{
    public class UserFilterModel : PaginationFilterModel
    {
        public string? userName { get; set; }
        public int userType { get; set; } = 0;
        public string? email { get; set; }
        public string? phone { get; set; }
    }
}
