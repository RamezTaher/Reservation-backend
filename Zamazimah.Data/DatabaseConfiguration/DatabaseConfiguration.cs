using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Zamazimah.Data.DatabaseConfiguration
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private readonly string DataConnectionKey = "DefaultConnection";
        public string GetDataConnectionString(IWebHostEnvironment env = null)
        {
            return GetConfiguration(env).GetConnectionString(DataConnectionKey);
        }
    }
}