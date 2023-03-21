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
    public class PilgrimsTripFilterModel : PaginationFilterModel
    {
        public string? TripNumber { get; set; }
        public string? TransportCompanyName { get; set; }
        public DateTime? Date { get; set; }
        public int? PilgrimsNumber { get; set; }
        public string? EntryPoint { get; set; }
        public string? HousingContractCode { get; set; }
        public int TripStatus { get; set; } = 0;
        public string? VehiclePlateNumber { get; set; } = null;
        public int? TransportCompanyId { get; set; } = null;
        public string? DistributorId { get; set; } = null;
        public DateTime? DateFrom { get; set; } = null;
        public DateTime? DateTo { get; set; } = null;
    }
}
