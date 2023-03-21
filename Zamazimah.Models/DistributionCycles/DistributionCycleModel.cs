using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.DistributionCycles
{
    public class DistributionCycleModel : BaseGetModel
    {
        public DistributionCycleModel()
        {
            HousingContracts = new List<DistributionCycleModel_HousingContract>();
            Distributor = new DistributionCycleModel_Distributor();
            Driver = new DistributionCycleModel_Driver();
            Vehicle = new DistributionCycleModel_Vehicle();
        }
        public string DistributionCycleNumber { get; set; }
        public DateTime DistributionDate { get; set; }
        public int TotalQuantity { get; set; }
        public int ReturnQuantity { get; set; }
        public string DistributorId { get; set; }
        public string DriverId { get; set; }
        public int VehicleId { get; set; }
        public IEnumerable<DistributionCycleModel_HousingContract> HousingContracts { get; set; }
        public DistributionCycleModel_Distributor Distributor { get; set; }
        public DistributionCycleModel_Driver Driver { get; set; }
        public DistributionCycleModel_Vehicle Vehicle { get; set; }
        public DistributionCycleModel_Store Store { get; set; }
        public DistributionCycleStatus Status { get; set; }
        public DateTime? StartCycleDate { get; set; }
        public DateTime? EndCycleDate { get; set; }
        public int CycleDuration { get { return (EndCycleDate != null && StartCycleDate != null) ? (EndCycleDate.Value - StartCycleDate.Value).Minutes : 0;  } }
    }
    public class DistributionCycleModel_HousingContract
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }
    public class DistributionCycleModel_Distributor
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class DistributionCycleModel_Driver
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class DistributionCycleModel_Vehicle
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }
    public class DistributionCycleModel_Store
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
