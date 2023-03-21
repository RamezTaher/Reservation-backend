using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.PilgrimsTrips
{
    public class CreatePilgrimsTripModel
    {
        public string TripNumber { get; set; }
        public string TransportCompanyName { get; set; }
        public DateTime Date { get; set; }
        public int PilgrimsNumber { get; set; }
        public string EntryPoint { get; set; }
        public int? HousingContractId { get; set; }
    }

    public class CreateUrgentTripPilgrimsTripModel
    {
        public string TripNumber { get; set; }
        public int PilgrimsNumber { get; set; }
        public int DistributedQuantity { get; set; }

        public string? Description { get; set; }
        public PilgrimTripType? Type { get; set; }
        public string? VehiclePlateNumber { get; set; }
    }
}
