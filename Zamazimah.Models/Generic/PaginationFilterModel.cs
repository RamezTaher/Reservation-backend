using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Models.Generic
{
    public class PaginationFilterModel
    {
        public int page { get; set; } = 1;
        public int take { get; set; } = 30;
    }
}
