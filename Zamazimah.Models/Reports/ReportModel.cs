using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.Reports
{
    public class ReportModel : BaseGetModel
    {
        public int? ReportTypeId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? ReportTypeName { get; set; }
        public ReportPriority Priority { get; set; }
        public ReportStatus Status { get; set; }
        public DateTime? ClosingDate { get; set; }
    }
}
