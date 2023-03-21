using Zamazimah.Models.Generic;

namespace Zamazimah.Models.DistributionContracts
{
    public class DistributionContractFilterModel : PaginationFilterModel
    {
        public string? Code { get; set; }
        public string? DistributionCompanyName { get; set; }
        public DateTime? SignatureDate { get; set; }
        public DateTime? GregorianStartDate { get; set; }
        public DateTime? GregorianEndDate { get; set; }
        public int? NumberOfDrivers { get; set; }
        public int? NumberOfVehicles { get; set; }
    }
}
