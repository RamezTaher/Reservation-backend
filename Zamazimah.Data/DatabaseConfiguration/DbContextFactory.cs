using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Zamazimah.Data.Context;
using System;

namespace Zamazimah.Data.DatabaseConfiguration
{
    /// <summary>
    /// This factory is provided so that the EF Core tools can build a full context
    /// without having to have access to where the DbContext is being created (i.e.
    /// in the UI layer).
    /// </summary>
    /// <remarks>
    /// Please see the following URL for more information:
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#using-idbcontextfactorytcontext
    /// </remarks>
    public class DbContextFactory : IDesignTimeDbContextFactory<ZamazimahContext>
    {
        private static string DataConnectionString => new DatabaseConfiguration().GetDataConnectionString();

        public ZamazimahContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ZamazimahContext>();
            optionsBuilder.UseNpgsql(DataConnectionString,
            options => options.SetPostgresVersion(new Version(9, 6)));
            return new ZamazimahContext(optionsBuilder.Options);
        }
    }
}