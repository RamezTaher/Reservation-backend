using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Models.DistributionCycles
{
    public class AcceptDistributionCodeModel
    {
        public string DistributionCode { get; set; }
        public int HousingContractId { get; set; }
    }
    public class ResendDistributionCodeModel
    {
        public int HousingContractId { get; set; }
    }
}
