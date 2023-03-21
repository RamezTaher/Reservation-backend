using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.DistributionCycles
{
    public class DistributionCycleFilterModel : PaginationFilterModel
    {
        public string? DistributionCycleNumber { get; set; }
        public DateTime? DistributionDate { get; set; } = null;
        public DateTime? DistributionDateFrom { get; set; } = null;
        public DateTime? DistributionDateTo { get; set; } = null;
        public string? DistributorId { get; set; }
        public string? DriverId { get; set; }
        public int? VehicleId { get; set; }
        public int? HousingContractId { get; set; }
        public int? DistributionCyclesId { get; set; }
        public bool IsDistributorCycle { get; set; } = false;
        public int Status { get; set; } = 0;
        public int? TotalQuantity { get; set; } = null;
    }
}
