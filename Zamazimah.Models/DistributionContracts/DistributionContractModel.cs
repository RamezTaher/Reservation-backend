using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;

namespace Zamazimah.Models.DistributionContracts
{
    public class DistributionContractModel : BaseGetModel
    {
        public string Code { get; set; }
        public string DistributionCompanyName { get; set; }
        public DateTime SignatureDate { get; set; }
        public DateTime HijriStartDate { get; set; }
        public DateTime GregorianStartDate { get; set; }

        public DateTime HijrieEndDate { get; set; }
        public DateTime GregorianEndDate { get; set; }
        public int NumberOfDrivers { get; set; }
        public int NumberOfVehicles { get; set; }
    }
}
