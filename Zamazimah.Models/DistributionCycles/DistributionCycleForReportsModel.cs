using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Models.DistributionCycles
{
    public class DistributionCycleForReportsModel
    {
        public int Id { get; set; }
        public string DistributorFullName { get; set; }
        public string ResponsableFullName { get; set; }
        public DateTime DistributionDate { get; set; }
        public DateTime? EndCycleDate { get; set; }
        public int PilgrimsNumber { get; set; }
        public int Quantity { get; set; }
        public int Cumulative { get; set; }
        public string? DistributionCode { get; set; }
        public string? HousingContractCode { get; set; }
        public string HousingName { get; set; }
    }
}
