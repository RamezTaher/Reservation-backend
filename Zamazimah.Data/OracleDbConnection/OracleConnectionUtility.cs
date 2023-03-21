using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Zamazimah.Data.OracleDbConnection
{
    public static class OracleConnectionUtility
    {

        public static IEnumerable<T> GetFromQuery<T>(string query)
        {
            IDbConnection connection = OracleConnectionManager.GetConnection();
            var list = connection.Query<T>(query).ToList();
            OracleConnectionManager.CloseConnection(connection);
            return list;
        }

        public static IEnumerable<OracleTrip> GetOracleHajjTrips()
        {
            var toDay = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);
            var beforeYesterday = DateTime.Now.AddDays(-2);
            string query = "select * from V_BUSES_TRIP WHERE MANIFEST_TRIP_DATE = '" + toDay.ToString("dd/MM/yyyy") + "' OR MANIFEST_TRIP_DATE = '" + yesterday.ToString("dd/MM/yyyy") + "' OR MANIFEST_TRIP_DATE = '" + beforeYesterday.ToString("dd/MM/yyyy")+"'";
            var result = OracleConnectionUtility.GetFromQuery<OracleTrip>(query);
            return result;
        }

        public static IEnumerable<OracleHouseContract> GetOracleHouseContracts()
        {
            string query = "select * from V_HOUSES";
            var result = OracleConnectionUtility.GetFromQuery<OracleHouseContract>(query);
            return result;
        }

    }
}
