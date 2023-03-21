using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Models.DistributorInventory
{
    public class AddDistributorInventoryModel
    {
        public string DistributorId { get; set; }
        public int AddedQuantity { get; set; }
    }
    public class EmptifyDistributorInventoryModel
    {
        public string DistributorId { get; set; }
    }
}
