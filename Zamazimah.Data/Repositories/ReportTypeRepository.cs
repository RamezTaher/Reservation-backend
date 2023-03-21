using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;

namespace Zamazimah.Data.Repositories
{
	public class ReportTypeRepository : BaseRepository<ReportType>, IReportTypeRepository
	{
		public ReportTypeRepository(ZamazimahContext context) : base(context)
		{
		}

	}

	public interface IReportTypeRepository : IRepository<ReportType>
	{
	}
}