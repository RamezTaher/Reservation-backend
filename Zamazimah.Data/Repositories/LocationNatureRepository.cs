using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;

namespace Zamazimah.Data.Repositories
{
	public class LocationNatureRepository : BaseRepository<LocationNature>, ILocationNatureRepository
	{
		public LocationNatureRepository(ZamazimahContext context) : base(context)
		{
		}

	}

	public interface ILocationNatureRepository : IRepository<LocationNature>
	{
	}
}