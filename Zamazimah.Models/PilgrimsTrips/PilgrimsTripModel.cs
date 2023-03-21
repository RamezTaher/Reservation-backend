using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Entities;
using Zamazimah.Models.Generic;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.PilgrimsTrips
{
    public class PilgrimsTripModel : BaseGetModel
    {
        public int Id { get; set; }
        public string TripNumber { get; set; }
        public string TransportCompanyName { get; set; }
        public DateTime Date { get; set; }
        public int PilgrimsNumber { get; set; }
        public string EntryPoint { get; set; }
        public string HousingContractCode { get; set; }
        public int DistributedQuantity { get; set; }
        public int QuantityNeeded { get { return PilgrimsNumber * 2; } }
        public int DistributionDifference { get { return DistributedQuantity - QuantityNeeded; } }
        public bool IsDistributionDone { get; set; }
        public string? VehiclePlateNumber { get; set; }
        public bool IsYesterday { get; set; }
        public PilgrimTripType? Type { get; set; }
        public string DistributorName { get; set; }
    }
}
