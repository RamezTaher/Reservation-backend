using Microsoft.EntityFrameworkCore;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.HousingContracts;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Data.Repositories
{
	public class HousingContractRepository : BaseRepository<HousingContract>, IHousingContractRepository
	{
		public HousingContractRepository(ZamazimahContext context) : base(context)
		{
		}

		public ResultApiModel<IEnumerable<HousingContractModel>> GetWithPagination(HousingContractFilterModel filter)
		{

			var q = dbSet.Where(hc => 
				(hc.Code.Contains(filter.Code) || filter.Code == null || filter.Code == string.Empty)
				&&
				(hc.HousingName.Contains(filter.HousingName) || filter.HousingName == null || filter.HousingName == string.Empty)
				&&
				(hc.Location.Contains(filter.Location) || filter.Location == null || filter.Location == string.Empty)
				&&
				((hc.StartDate != null && hc.StartDate.Value.Date == filter.StartDate) || filter.StartDate == null)
				&&
				((hc.EndDate != null && hc.EndDate.Value.Date == filter.EndDate) || filter.EndDate == null)
				&&
				((hc.PilgrimsNumber == filter.PilgrimsNumber && filter.PilgrimsNumberComparisonType == ComparisonType.Equal) 
				||
				(hc.PilgrimsNumber > filter.PilgrimsNumber && filter.PilgrimsNumberComparisonType == ComparisonType.Sup) 
				||
				(hc.PilgrimsNumber < filter.PilgrimsNumber && filter.PilgrimsNumberComparisonType == ComparisonType.Inf) || filter.PilgrimsNumber == null)
				&&
				(hc.LocationNatureId == filter.LocationNatureId || filter.LocationNatureId == null)
			).Select(hc => new HousingContractModel
			{
				Id = hc.Id,
				HousingName = hc.HousingName,
				Code = hc.Code,
				PilgrimsNumber = hc.PilgrimsNumber,
				Location = hc.Location,
				StartDate = hc.StartDate,
				EndDate = hc.EndDate,
				Created = hc.Created,
				Modified = hc.Modified,
				LocationNatureId = hc.LocationNatureId,
				LocationNatureName = hc.LocationNature.Name,
				Latitude = hc.Latitude,
				Longitude = hc.Longitude,
				DistributedQuantity = hc.DistributionCycleHousingContracts.Where(x=>x.IsDistributionCodeAccepted).Sum(x=>x.Quantity),
				PlannedQuantity = hc.DistributionCycleHousingContracts.Where(x => !x.IsDistributionCodeAccepted).Sum(x => x.Quantity),
			});
			return this.Paginate(q, filter.page, filter.take);
		}

		public HousingContract GetDetailsById(int id)
        {
			return dbSet.Include(d=>d.Center).Include(x=>x.LocationNature).Include(x => x.Responsable).Include(x => x.City).FirstOrDefault(x=>x.Id == id);
        }

	}

	public interface IHousingContractRepository : IRepository<HousingContract>
	{
		public ResultApiModel<IEnumerable<HousingContractModel>> GetWithPagination(HousingContractFilterModel filter);
		HousingContract GetDetailsById(int id);
	}
}