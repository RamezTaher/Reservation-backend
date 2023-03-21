using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Zamazimah.Entities.Identity;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Entities
{
    public class PilgrimsTrip : BaseAuditClass
    {
        public string TripNumber { get; set; }
        public string? TransportCompanyName { get; set; }
        public int? HousingContractId { get; set; }
        public virtual HousingContract HousingContract { get; set; }
        public DateTime Date { get; set; }
        public int PilgrimsNumber { get; set; }
        public string? EntryPoint { get; set; }
        public int DistributedQuantity { get; set; }
        public string? OverDistributionReason { get; set; }
        public bool IsDistributionDone { get; set; }
        public DateTime? DistributionCompletedDate { get; set; }
        public string? DistributorId { get; set; }
        public ApplicationUser Distributor { get; set; }
        public bool IsImportedFromZamazimahDB { get; set; }

        public int? TransportCompanyId { get; set; }
        public virtual TransportCompany TransportCompany { get; set; }
        public string? VehiclePlateNumber { get; set; }

        public string? Description { get; set; }
        public PilgrimTripType? Type { get; set; }
        public string? TripImage { get; set; }
    }
}
