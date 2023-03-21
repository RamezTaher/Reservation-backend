using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Entities.Identity;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Entities
{
    public class Report : BaseAuditClass
    {
        public int? ReportTypeId { get; set; }
        public virtual ReportType ReportType { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public ReportPriority Priority { get; set; }
        public ReportStatus Status { get; set; }

        public DateTime? ClosingDate { get; set; }

        public string? FileUrl { get; set; }

    }
}
