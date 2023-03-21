using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Entities.Identity;

namespace Zamazimah.Entities
{
    public class DistributorInventory : BaseAuditClass
    {
        public string DistributorId { get; set; }
        public ApplicationUser Distributor { get; set; }
        public int AddedQuantity { get; set; }
        public int ConsumedQuantity { get; set; }
        public bool IsReturn { get; set; }

    }
}
