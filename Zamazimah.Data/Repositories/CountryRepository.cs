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
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(ZamazimahContext context) : base(context) { }

        public List<Country> GetAllCountries()
        {
            return dbSet.ToList();
        }
    }
    public interface ICountryRepository : IRepository<Country>
    {
        List<Country> GetAllCountries();
    }
}
