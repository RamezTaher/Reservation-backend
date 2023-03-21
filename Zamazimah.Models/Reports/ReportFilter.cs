using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;

namespace Zamazimah.Models.Reports
{
    public class ReportFilter : PaginationFilterModel
    {
        public string? Query { get; set; } = null;
        public DateTime? Date { get; set; } = null;
        public DateTime? DateFrom { get; set; } = null;
        public DateTime? DateTo { get; set; } = null;

        public int? ReportTypeId { get; set; } = null;
        public int Priority { get; set; } = 0;
        public int Status { get; set; } = 0;
    }
}
