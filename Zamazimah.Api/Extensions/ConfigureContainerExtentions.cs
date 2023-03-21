using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Linq;
using Zamazimah.Data;
using Zamazimah.Data.Context;
using Zamazimah.Data.DatabaseConfiguration;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Data.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Services;

namespace Zamazimah.Api.Extensions
{
    public static class ConfigureContainerExtensions
    {
        private const string ContentRootPathToken = "%CONTENT_ROOT_PATH%";
        public static void AddDbContext(this IServiceCollection serviceCollection, IWebHostEnvironment env, string contentPath, string dataConnectionString = null)
        {
            serviceCollection.AddDbContext<ZamazimahContext>(options =>
               options.UseNpgsql(dataConnectionString ?? GetDataConnectionStringFromConfig(contentPath, env),
               options => options.SetPostgresVersion(new Version(9, 6))));



            serviceCollection.AddIdentity<ApplicationUser, Permission>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ZamazimahContext>()
                .AddEntityFrameworkStores<ZamazimahContext>()
                .AddDefaultTokenProviders();
        }
        //public static readonly LoggerFactory MyLoggerFactory
        //    = new LoggerFactory(new[]
        //    {
        //            new ConsoleLoggerProvider(("")
        //                => category == DbLoggerCategory.Database.Command.Name
        //                   && level == LogLevel.Information, true)
        //    });

        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            var repositoryAssembly = typeof(GroupRepository).Assembly;
            var typesFromAssemblies = repositoryAssembly.GetExportedTypes().Where(x => !x.IsAbstract && x.IsClass && x.FullName.Contains("Repository"));
            foreach (var type in typesFromAssemblies)
            {
                Type typeInterface = type.GetInterfaces().Where(x => x.FullName.Contains(type.Name)).FirstOrDefault();
                if (typeInterface != null)
                {
                    serviceCollection.AddScoped(typeInterface, type);
                }
            }
        }

        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {

            var repositoryAssembly = typeof(GroupService).Assembly;
            var typesFromAssemblies = repositoryAssembly.GetExportedTypes().Where(x => !x.IsAbstract && x.IsClass && x.FullName.Contains("Service"));
            foreach (var type in typesFromAssemblies)
            {
                Type typeInterface = type.GetInterfaces().FirstOrDefault();
                if (typeInterface != null)
                {
                    serviceCollection.AddTransient(typeInterface, type);
                }
            }
        }

        private static string GetDataConnectionStringFromConfig(string contentPath, IWebHostEnvironment env)
        {
            string connectionString = new DatabaseConfiguration().GetDataConnectionString(env);
            Console.Write(connectionString);
            if (connectionString.Contains(ContentRootPathToken))
            {
                connectionString = connectionString.Replace(ContentRootPathToken, contentPath);
            }
            return connectionString;
        }
    }

}