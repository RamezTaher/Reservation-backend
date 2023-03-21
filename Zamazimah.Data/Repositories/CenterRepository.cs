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
    public class CenterRepository : BaseRepository<Center>, ICenterRepository
    {
        public CenterRepository(ZamazimahContext context) : base(context) { }

    }
    public interface ICenterRepository : IRepository<Center>
    {
    }
}
