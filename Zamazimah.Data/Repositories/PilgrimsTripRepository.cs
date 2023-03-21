using Microsoft.EntityFrameworkCore;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Entities.Identity;
using Zamazimah.Generic.Models;
using Zamazimah.Models.PilgrimsTrips;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Data.Repositories
{
	public class PilgrimsTripRepository : BaseRepository<PilgrimsTrip>, IPilgrimsTripRepository
	{
		public PilgrimsTripRepository(ZamazimahContext context) : base(context)
		{
		}

		public ResultApiModel<IEnumerable<PilgrimsTripModel>> GetWithPagination(PilgrimsTripFilterModel filter, ApplicationUser user)
		{
			var q = dbSet.Include(x=>x.HousingContract).Where(hc => 
			(hc.TripNumber.Contains(filter.TripNumber) || filter.TripNumber == null || filter.TripNumber == String.Empty)
			&&
			((hc.HousingContractId != null && hc.HousingContract.Code.Contains(filter.HousingContractCode)) || filter.HousingContractCode == null || filter.HousingContractCode == String.Empty)
			&&
			(hc.Date.Date == filter.Date || filter.Date == null)
			&&
			(hc.EntryPoint.Contains(filter.EntryPoint) || filter.EntryPoint == null || filter.EntryPoint == String.Empty)
			&&
			((user.UserType == PredefinedUserType.DISTRIBUTOR && ( hc.Date.Date == DateTime.Now.Date || hc.Date.Date == DateTime.Now.AddDays(-1).Date)) || user.UserType != PredefinedUserType.DISTRIBUTOR)
			&&
			(
			(hc.IsDistributionDone  && filter.TripStatus == 4)
			||
			((hc.IsDistributionDone && (hc.PilgrimsNumber * 2) == hc.DistributedQuantity && (ComparisonType)filter.TripStatus == ComparisonType.Equal)
			||
			(hc.IsDistributionDone && (hc.PilgrimsNumber * 2) < hc.DistributedQuantity && (ComparisonType)filter.TripStatus == ComparisonType.Sup)
			||
			(hc.IsDistributionDone && (hc.PilgrimsNumber * 2) > hc.DistributedQuantity && (ComparisonType)filter.TripStatus == ComparisonType.Inf)) || filter.TripStatus == 0)
			&&
			((hc.VehiclePlateNumber != null && hc.VehiclePlateNumber.Contains(filter.VehiclePlateNumber)) || filter.VehiclePlateNumber == null || filter.VehiclePlateNumber == String.Empty)
			&&
			(hc.TransportCompanyId ==  filter.TransportCompanyId || filter.TransportCompanyId == null)
			&&
			( hc.DistributorId == filter.DistributorId || filter.DistributorId == null )
			&&
			((filter.DateFrom != null && hc.DistributionCompletedDate >= filter.DateFrom.Value) || filter.DateFrom == null)
			&&
			((filter.DateTo != null && hc.DistributionCompletedDate <= filter.DateTo.Value) || filter.DateTo == null)
			).OrderByDescending(x=>x.Date).ThenByDescending(x=>x.Id).Select(hc => new PilgrimsTripModel
			{
				Id = hc.Id,
				TripNumber = hc.TripNumber,
				Date = hc.DistributionCompletedDate ?? hc.Date,
				EntryPoint = hc.EntryPoint,
				PilgrimsNumber = hc.PilgrimsNumber,
				TransportCompanyName = hc.TransportCompanyId != null ? hc.TransportCompany.Name : hc.TransportCompanyName,
				HousingContractCode = (hc.HousingContractId != null) ? hc.HousingContract.Code : "",
				IsDistributionDone = hc.IsDistributionDone,
				DistributedQuantity = hc.DistributedQuantity,
				VehiclePlateNumber = hc.VehiclePlateNumber,
				IsYesterday = hc.Date.Date == DateTime.Now.AddDays(-1).Date,
				Type = hc.Type,
				DistributorName = hc.Distributor != null ? hc.Distributor.FullName : "",
			});
			var totalBottles = q.Sum(x => x.DistributedQuantity);
			var numberOfTripsDone = q.Count(x => x.IsDistributionDone);
			var result = this.Paginate(q, filter.page, filter.take);
			result.NumberOfDistributedBottles = totalBottles;
			result.NumberOfTripsDone = numberOfTripsDone;
			return result;
		}

		public PilgrimsTrip GetDetailsById(int id)
		{
			return dbSet.Include(x => x.Distributor).FirstOrDefault(x => x.Id == id);
		}


		public List<DistributorPerformanceModel> GetDistributorsPerformance()
        {
			var result = context.Users.Where(x => x.UserType == PredefinedUserType.DISTRIBUTOR && x.DistributionPointId == 2).Select(x=>new DistributorPerformanceModel
			{
				DistributorId = x.Id,
				DistributorName = x.FullName,
				NumberOfTrips = context.PilgrimsTrips.Where(t=>t.DistributorId == x.Id).Count(),
				NumberOfBottles = context.PilgrimsTrips.Where(t => t.DistributorId == x.Id).Sum(s=>s.DistributedQuantity),
				CumulativePerformance = context.PilgrimsTrips.Where(t => t.DistributorId == x.Id).Sum(s=>s.DistributedQuantity - (s.PilgrimsNumber * 2)),
				Inventory = context.DistributorInventories.Where(j=>j.DistributorId == x.Id).Sum(i=>i.AddedQuantity - i.ConsumedQuantity)
			}).ToList();
			return result;
        }

	}

	public interface IPilgrimsTripRepository : IRepository<PilgrimsTrip>
	{
		ResultApiModel<IEnumerable<PilgrimsTripModel>> GetWithPagination(PilgrimsTripFilterModel filter, ApplicationUser user);
		PilgrimsTrip GetDetailsById(int id);
		List<DistributorPerformanceModel> GetDistributorsPerformance();
	}
}