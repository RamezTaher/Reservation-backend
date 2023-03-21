using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Data.OracleDbConnection
{
    public class OracleHouseContract
    {
        public string HOUSE_REG_NO { get; set; }
        public string? HOUSE_COMMERCIAL_NAME_AR { get; set; }
        public string? HOUSE_ZONE { get; set; }
        public string? HOUSE_ADDRESS_1 { get; set; }
        public string? HOUSE_ADDRESS_2 { get; set; }
        public string? HOUSE_WASEL_BUILDING_NO { get; set; }
        public string? HOUSE_WASEL_POSTAL_CODE { get; set; }
        public string? HOUSE_WASEL_ADDITIONAL_NO { get; set; }
        public int الوصول { get; set; }
        public int الاستيعاب { get; set; }
        public string? link { get; set; }
    }
}
