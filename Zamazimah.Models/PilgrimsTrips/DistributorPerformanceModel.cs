using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Models.PilgrimsTrips
{
    public class DistributorPerformanceModel
    {
        public string DistributorId { get; set; }
        public string DistributorName { get; set; }
        public int NumberOfTrips { get; set; }
        public int NumberOfBottles { get; set; }
        public int CumulativePerformance { get; set; }
        public int Inventory { get; set; }
    }
}
