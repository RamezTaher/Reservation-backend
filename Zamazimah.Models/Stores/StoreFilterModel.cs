using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;

namespace Zamazimah.Models.Stores
{
    public class StoreFilterModel : PaginationFilterModel
    {
        public string? StoreNumber { get; set; }
        public string? Address { get; set; }
        public string? Responsable { get; set; }
        public int? Capacity { get; set; }
        public int? AvailableQuantity { get; set; }
        public int? ReservedQuantity { get; set; }
        public int? ActualQuantity { get; set; }
        public int? AlertQuantity { get; set; }
    }
}
