using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.HousingContracts
{
	public class HousingContractFilterModel : PaginationFilterModel
	{
		public string? Code { get; set; }
		public string? HousingName { get; set; }
		public string? Location { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int? PilgrimsNumber { get; set; }
		public int? LocationNatureId { get; set; }

		public ComparisonType PilgrimsNumberComparisonType { get; set; } = ComparisonType.Equal;
	}
}
