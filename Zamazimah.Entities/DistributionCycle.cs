using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Entities.Identity;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Entities
{
    public class DistributionCycle : BaseAuditClass
    {
        public DistributionCycle()
        {
            DistributionCycleHousingContracts = new List<DistributionCycleHousingContract>();
        }
        public string DistributionCycleNumber { get; set; }
        public DateTime DistributionDate { get; set; }
        public DateTime? StartCycleDate { get; set; }
        public DateTime? EndCycleDate { get; set; }
        public string DistributorId { get; set; }
        public virtual ApplicationUser Distributor { get; set; }

        public string DriverId { get; set; }
        public virtual ApplicationUser Driver { get; set; }

        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<DistributionCycleHousingContract> DistributionCycleHousingContracts { get; set; }
        public DistributionCycleStatus Status { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public bool Seen { get; set; }
        public string? DistributionImageUrl { get; set; }
    }
}
