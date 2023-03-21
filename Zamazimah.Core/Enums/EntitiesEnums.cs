using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Core.Enums
{
    public class EntitiesEnums
    {
        public enum PredefinedUserType
        {
            ADMIN = 1,
            REPRESENTATIVE,
            DISTRIBUTOR,
            PLANNER,
            DRIVER,
            SUPERVISOR,
            SUPERVISOR_REPORTS,
        }
        public enum EntityType
        {
            DistributionContract = 1,
            DistributionCycle,
            HousingContract,
            PilgrimsTrip,
            Report,
            Store,
            Vehicle
        }
        public enum DistributionCycleStatus
        {
            New = 1,
            Planned,
            Completed
        }

        public enum ReportPriority
        {
            Urgent = 1,
            Important,
            Normal,
            Notice
        }

        public enum ReportStatus
        {
            New = 1,
            InProgress,
            Closed
        }

        public enum ComparisonType
        {
            Equal = 1,
            Sup,
            Inf
        }

        public enum PilgrimTripType
        {
            Urgent = 1,
            LandTrip,
        }
    }
}
