using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zamazimah.Data.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(ZamazimahContext context) : base(context) { }

        public List<City> GetAllCities()
        {
            return dbSet.ToList();
        }
    }
    public interface ICityRepository : IRepository<City>
    {
        List<City> GetAllCities();
    }
}
