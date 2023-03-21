using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;

namespace Zamazimah.Models.HousingContracts
{
    public class HousingContractModel : BaseGetModel
    {
        public string Code { get; set; }
        public string HousingName { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PilgrimsNumber { get; set; }
        public int? LocationNatureId { get; set; }
        public string LocationNatureName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int DistributedQuantity { get; set; }
        public int PlannedQuantity { get; set; }
        public int Duration { get { return EndDate != null && StartDate != null ? (EndDate.Value - StartDate.Value).Days : 0; } }
    }
    public class HousingContractAutocompleteModel : HousingContractModel
    {
        public string Text { get; set; }
        public int SuggestedQuantityToDistribue { get; set; }
        public int Order { get; set; }
    }
}
