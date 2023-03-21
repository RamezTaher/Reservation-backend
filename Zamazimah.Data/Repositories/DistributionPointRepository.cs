using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;

namespace Zamazimah.Data.Repositories
{
	public class DistributionPointRepository : BaseRepository<DistributionPoint>, IDistributionPointRepository
	{
		public DistributionPointRepository(ZamazimahContext context) : base(context)
		{
		}

	}

	public interface IDistributionPointRepository : IRepository<DistributionPoint>
	{
	}
}