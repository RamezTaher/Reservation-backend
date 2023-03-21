using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;

namespace Zamazimah.Models.DistributionCycles
{
    public class CreateDistributionCycleModel
    {
        public CreateDistributionCycleModel()
        {
            HounsingContracts = new List<CreateDistributionCycleModel_HounsingContract>();
        }
        public int TotalQuantity { get; set; }
        public string DistributorId { get; set; }
        public string DriverId { get; set; }
        public int VehicleId { get; set; }
        public int StoreId { get; set; }
        public DateTime DistributionDate { get; set; }
        public List<CreateDistributionCycleModel_HounsingContract> HounsingContracts { get; set; }

    }

    public class CreateDistributionCycleModel_HounsingContract
    {
        public int HousingContractId { get; set; }
        public int Quantity { get; set; }
        public int NumberOfDays { get; set; }
    }
}
