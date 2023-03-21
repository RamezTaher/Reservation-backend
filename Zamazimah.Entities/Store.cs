using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Entities
{
    public class Store : BaseAuditClass
    {
        public Store()
        {
            DistributionCycles = new List<DistributionCycle>();
        }
        public string StoreNumber { get; set; }
        public string? Title { get; set; }
        public string Address { get; set; }
        public string ResponsableFirstName { get; set; }
        public string ResponsableLastName { get; set; }
        public string ResponsableEmail { get; set; }
        public string ResponsablePhone { get; set; }
        public int Capacity { get; set; }
        public int AlertQuantity { get; set; }
        public bool ForEntryPoint { get; set; }
        public virtual ICollection<DistributionCycle> DistributionCycles { get; set; }
    }
}
