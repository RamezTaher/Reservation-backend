using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Entities.Identity;

namespace Zamazimah.Entities
{
    public class DistributionCycleHousingContract
    {
        public int Id { get; set; }
        public int HousingContractId { get; set; }
        public virtual HousingContract HousingContract { get; set; }

        public int DistributionCycleId { get; set; }
        public virtual DistributionCycle DistributionCycle { get; set; }
        public int Quantity { get; set; }
        public int NumberOfPilgrims { get; set; }
        public string? DistributionCode { get; set; }
        public bool IsDistributionCodeAccepted { get; set; }
        public bool IsReturnAccepted { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public int NumberOfDays { get; set; }
        public int ReturnedQuantity { get; set; }
        public string? ReportUrl { get; set; }
    }
}
