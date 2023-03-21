using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.Reports
{
    public class CreateReportModel
    {
        public int? ReportTypeId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ReportPriority Priority { get; set; }
    }
}
