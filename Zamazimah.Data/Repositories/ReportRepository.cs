using Microsoft.EntityFrameworkCore;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.Reports;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Data.Repositories
{
	public class ReportRepository : BaseRepository<Report>, IReportRepository
	{
		public ReportRepository(ZamazimahContext context) : base(context)
		{
		}

		public ResultApiModel<IEnumerable<ReportModel>> GetWithPagination(ReportFilter filter)
		{
			var q = dbSet.Include(x=>x.User).Include(x=>x.ReportType)
				.Where(hc => (hc.Title.Contains(filter.Query) ||
				 hc.Text.Contains(filter.Query) ||
				 filter.Query == null)
				 &&
				 ((filter.Date != null && hc.Created.Date == filter.Date.Value.Date) || filter.Date == null)
				 &&
				 ( hc.ReportTypeId == filter.ReportTypeId || filter.ReportTypeId == null)
				 &&
				 (hc.Status == (ReportStatus)filter.Status || filter.Status == 0)
				 &&
				 (hc.Priority == (ReportPriority)filter.Priority || filter.Priority == 0)
				 &&
				 ((filter.DateFrom != null && hc.Created.Date >= filter.DateFrom.Value.Date) || filter.DateFrom == null)
				 &&
				 ((filter.DateTo != null && hc.Created.Date <= filter.DateTo.Value.Date) || filter.DateTo == null)
				)
				.Select(hc => new ReportModel
			{
				Id = hc.Id,
				ReportTypeId = hc.ReportTypeId,
				ReportTypeName = hc.ReportType != null ? hc.ReportType.Name : null,
				Title = hc.Title,
				Created = hc.Created,
				Modified = hc.Modified,
				Text = hc.Text,
				ClosingDate = hc.ClosingDate,
				Priority = hc.Priority,
				UserId = hc.UserId,
				Status = hc.Status,
				UserName = hc.User != null ? hc.User.FullName : null,
			});
			return this.Paginate(q, filter.page, filter.take);
		}

		public Report GetDetailsById(int id)
        {
			return dbSet.Include(x=>x.ReportType).Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

	}

	public interface IReportRepository : IRepository<Report>
	{
		public ResultApiModel<IEnumerable<ReportModel>> GetWithPagination(ReportFilter filter);
		Report GetDetailsById(int id);
	}
}