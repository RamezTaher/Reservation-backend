using Zamazimah.Models.Generic;

namespace Zamazimah.Models.Identity
{
    public class DistributorFilterModel : PaginationFilterModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? IdentityExpiration { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? NominationDate { get; set; }
        public int? DistributionPointId { get; set; }
    }
}
