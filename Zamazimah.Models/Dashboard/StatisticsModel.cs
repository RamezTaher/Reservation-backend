using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Models.Dashboard
{
    public class StatisticsModel
    {
        public int NumberOfCycles { get; set; }
        public double CycleDurationAverage { get; set; }
        public int NumberOfBottlesNeeded { get; set; }
        public int NumberOfDistributedBottles { get; set; }
        public int NumberOfDistributors { get; set; }
        public int NumberOfReports { get; set; }
        public int NumberOfResolvedReports { get; set; }
        public int NumberOfInProgressReports { get; set; }
        public int ToDayDistributedBottles { get; set; }
        public int ToDayDistributedBottlesNeeded { get; set; }
        public decimal DelivryRate { get { return ToDayDistributedBottlesNeeded > 0 ? (((decimal)ToDayDistributedBottles / (decimal)ToDayDistributedBottlesNeeded) * 100) : 0; } }

        public int NumberOfDistributedBottlesInDistributionPoints { get; set; }
        public int NumberOfTrips { get; set; }
        public int NumberOfTripsDistributed { get; set; }
        public decimal DistribtuionRateInDistributionPoints { get { return NumberOfTrips > 0 ? (((decimal)NumberOfTripsDistributed / (decimal)NumberOfTrips) * 100) : 0; } }
    }
}
