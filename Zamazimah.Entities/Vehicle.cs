using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Entities
{
    public class Vehicle: BaseAuditClass
    {
        public string PlateNumber { get; set; }
        public string FormNumber { get; set; }
        public DateTime FormExpiration { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime InsuranceExpiration { get; set; }
        public string Brand { get; set; }
        public string Age { get; set; }
        public decimal MaxLoad { get; set; }
    }
}
