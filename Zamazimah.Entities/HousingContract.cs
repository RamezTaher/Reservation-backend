using System;
using System.Collections.Generic;
using System.Text;
using Zamazimah.Entities.Identity;

namespace Zamazimah.Entities
{
    public class HousingContract : BaseAuditClass
    {
        public HousingContract()
        {
            DistributionCycleHousingContracts = new List<DistributionCycleHousingContract>();
        }
        public string Code { get; set; }
        public string HousingName { get; set; }
        public string? CommercialHousingName { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PilgrimsNumber { get; set; }
        public virtual ICollection<PilgrimsTrip> PilgrimsTrips { get; set; }
        public virtual ICollection<DistributionCycleHousingContract> DistributionCycleHousingContracts { get; set; }
        public int? LocationNatureId { get; set; }
        public virtual LocationNature LocationNature { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? ImageUrl { get; set; }

        public string? ResponsableId { get; set; }
        public virtual ApplicationUser Responsable { get; set; }

        public string? ResidencePermitNumber { get; set; }

        public int? CityId { get; set; }
        public virtual City City { get; set; }
        public string? WassilNumber { get; set; }
        public int? CenterId { get; set; }
        public virtual Center Center { get; set; }
        public bool IsImportedFromZamazimahDB { get; set; }
    }
}
