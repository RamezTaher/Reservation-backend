using Microsoft.EntityFrameworkCore;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Entities.Identity;
using Zamazimah.Generic.Models;
using Zamazimah.Models.Dashboard;
using Zamazimah.Models.DistributionCycles;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Data.Repositories
{
	public class DistributionCycleRepository : BaseRepository<DistributionCycle>, IDistributionCycleRepository
	{
		public DistributionCycleRepository(ZamazimahContext context) : base(context)
		{
		}

		public ResultApiModel<IEnumerable<DistributionCycleModel>> GetWithPagination(DistributionCycleFilterModel filter, ApplicationUser user)
		{
			var q = dbSet.Include(x=> x.DistributionCycleHousingContracts).Include(x => x.Distributor).Include(x => x.Store).Include(x => x.Driver).Include(x => x.Vehicle).Where(hc => 
			(hc.DistributionCycleNumber.Contains(filter.DistributionCycleNumber) || filter.DistributionCycleNumber == null || filter.DistributionCycleNumber == String.Empty)
			&& 
			(hc.DistributionCycleHousingContracts.Any(f=>f.HousingContractId == filter.HousingContractId) || filter.HousingContractId == null)
			&&
			(hc.DistributorId == filter.DistributorId || filter.DistributorId == null)
			&&
			(hc.DriverId == filter.DriverId || filter.DriverId == null)
			&&
			(hc.VehicleId == filter.VehicleId || filter.VehicleId == null)
			&&
			((filter.DistributionDate != null && hc.DistributionDate.Date == filter.DistributionDate.Value.Date) || filter.DistributionDate == null)
			&&
			((filter.DistributionDateFrom != null && hc.DistributionDate.Date >= filter.DistributionDateFrom.Value.Date) || filter.DistributionDateFrom == null)
			&&
			((filter.DistributionDateTo != null && hc.DistributionDate.Date <= filter.DistributionDateTo.Value.Date) || filter.DistributionDateTo == null)
			&&
			(filter.IsDistributorCycle && hc.DistributorId == user.Id || !filter.IsDistributorCycle)
			&&
			(hc.Status == (DistributionCycleStatus)filter.Status || filter.Status == 0)
			&&
			(hc.DistributionCycleHousingContracts.Sum(x=>x.Quantity) == filter.TotalQuantity || filter.TotalQuantity == null)
			).OrderByDescending(x=>x.Id).Select(hc => new DistributionCycleModel
			{
				Id = hc.Id,
				DistributionCycleNumber = hc.DistributionCycleNumber,	
				DistributionDate = hc.DistributionDate,	
				Modified = hc.Modified,
				Created = hc.Created,
				DistributorId = hc.DistributorId,
				DriverId = hc.DriverId,
				VehicleId = hc.VehicleId,
				Status = hc.Status,
				StartCycleDate = hc.StartCycleDate,
				EndCycleDate = hc.EndCycleDate,
				TotalQuantity = hc.DistributionCycleHousingContracts.Sum(x=>x.Quantity - x.ReturnedQuantity),
				ReturnQuantity = hc.DistributionCycleHousingContracts.Sum(x=>x.ReturnedQuantity),
				HousingContracts = hc.DistributionCycleHousingContracts.Select(d=> new DistributionCycleModel_HousingContract
                {
					Id = d.HousingContractId,
					Code = d.HousingContract.Code,
                }),
				Distributor = new DistributionCycleModel_Distributor
                {
					Id = hc.Distributor.Id,
					Code = hc.Distributor.Id,
					Name = hc.Distributor.FirstName + " " + hc.Distributor.LastName,
                },
				Driver = new DistributionCycleModel_Driver
                {
					Id = hc.Driver.Id,
					Code = hc.Driver.Id,
					Name = hc.Driver.FirstName + " " + hc.Driver.LastName,
				},
				Vehicle = new DistributionCycleModel_Vehicle
                {
					Id = hc.Vehicle.Id,
					Code = hc.Vehicle.PlateNumber,
				},
                Store = new DistributionCycleModel_Store
                {
                    Id = hc.Store.Id,
                    Code = hc.Store.StoreNumber,
                },
            });
			return this.Paginate(q, filter.page, filter.take);
		}

		public DistributionCycle GetDetailsById(int id)
		{
			return dbSet.Include(x => x.DistributionCycleHousingContracts).ThenInclude(d=>d.HousingContract).ThenInclude(x=>x.Responsable)
				.Include(x => x.Distributor)
                 .Include(x => x.Store)
                .Include(x => x.Driver).Include(x => x.Vehicle).FirstOrDefault(x=>x.Id == id);
		}

		public string GenerateDistributionCycleNumber()
        {
			var count = dbSet.Count();
			return (count + 1).ToString("D3");
		}


		public QuantitiesModel GetQuantities(DateTime date)
        {
			var result = new QuantitiesModel
			{
				ToDayQuantity = dbSet.Where(x=> x.StartCycleDate.Value.Date == date.Date && ( x.Status == DistributionCycleStatus.Planned || x.Status == DistributionCycleStatus.Completed))
				.SelectMany(s=>s.DistributionCycleHousingContracts.Where(w=>w.IsDistributionCodeAccepted)).Sum(f=>f.Quantity - f.ReturnedQuantity),
				PreviousQuantity = dbSet.Where(x => x.StartCycleDate.Value.Date < date.Date && (x.Status == DistributionCycleStatus.Planned || x.Status == DistributionCycleStatus.Completed))
				.SelectMany(s => s.DistributionCycleHousingContracts.Where(w => w.IsDistributionCodeAccepted)).Sum(f => f.Quantity - f.ReturnedQuantity),
			};

			return result;
        }

		public StatisticsModel GetStatistics()
		{
			var result = new StatisticsModel
			{
				CycleDurationAverage = dbSet.Average(f => ((f.EndCycleDate != null && f.StartCycleDate != null) ? (f.EndCycleDate - f.StartCycleDate).Value.Minutes : 0)),
				NumberOfBottlesNeeded = context.HousingContracts.Sum(x => x.PilgrimsNumber) * 3,
				NumberOfCycles = dbSet.Count(),
				NumberOfDistributedBottles = dbSet.Where(x => (x.Status == DistributionCycleStatus.Planned || x.Status == DistributionCycleStatus.Completed))
				.SelectMany(s => s.DistributionCycleHousingContracts.Where(w => w.IsDistributionCodeAccepted)).Sum(f => f.Quantity - f.ReturnedQuantity) * 40,
				NumberOfDistributors = context.Users.Count(d => d.UserType == PredefinedUserType.DISTRIBUTOR),
				NumberOfInProgressReports = context.Reports.Where(x => x.Status != ReportStatus.Closed).Count(),
				NumberOfReports = context.Reports.Count(),
				NumberOfResolvedReports = context.Reports.Where(x => x.Status == ReportStatus.Closed).Count(),
				ToDayDistributedBottles = context.DistributionCycleHousingContracts.Where(c => c.IsDistributionCodeAccepted == true && c.DistributionCycle.DistributionDate.Date == DateTime.Now.Date).Sum(s => s.Quantity - s.ReturnedQuantity),
				ToDayDistributedBottlesNeeded = context.DistributionCycleHousingContracts.Where(c => c.DistributionCycle.DistributionDate.Date == DateTime.Now.Date).Sum(s => s.Quantity - s.ReturnedQuantity),
				NumberOfDistributedBottlesInDistributionPoints = context.PilgrimsTrips.Where(x => x.IsDistributionDone).Sum(s=>s.DistributedQuantity),
				NumberOfTrips = context.PilgrimsTrips.Count(),
				NumberOfTripsDistributed = context.PilgrimsTrips.Where(d=>d.IsDistributionDone).Count(),
			};
			return result;
		}

		public ResultApiModel<IEnumerable<DistributionCycleForReportsModel>> GetForReportsWithPagination(DistributionCycleFilterModel filter, ApplicationUser user)
		{
			var q = context.DistributionCycleHousingContracts.Where(dc => (dc.DistributionCycle.DistributionCycleNumber.Contains(filter.DistributionCycleNumber) || filter.DistributionCycleNumber == null || filter.DistributionCycleNumber == String.Empty)
			&&
			(dc.HousingContractId == filter.HousingContractId || filter.HousingContractId == null)
			&&
			(dc.DistributionCycleId == filter.DistributionCyclesId || filter.DistributionCyclesId == null)
			&&
			(dc.DistributionCycle.DistributorId == filter.DistributorId || filter.DistributorId == null)
			&&
			(dc.DistributionCycle.DriverId == filter.DriverId || filter.DriverId == null)
			&&
			(dc.DistributionCycle.VehicleId == filter.VehicleId || filter.VehicleId == null)
			&&
			((filter.DistributionDate != null && dc.DistributionCycle.DistributionDate.Date == filter.DistributionDate.Value.Date) || filter.DistributionDate == null)
			&&
			(filter.IsDistributorCycle && dc.DistributionCycle.DistributorId == user.Id || !filter.IsDistributorCycle)
			&&
			(dc.DistributionCycle.Status != DistributionCycleStatus.New)).OrderByDescending(x => x.Id).Select(dc => new DistributionCycleForReportsModel
			{
				Id = dc.Id,
				DistributionDate = dc.DistributionCycle.DistributionDate,
				EndCycleDate = dc.AcceptanceDate ?? dc.DistributionCycle.EndCycleDate,
				DistributorFullName = dc.DistributionCycle.Distributor.FullName,
				HousingContractCode = dc.HousingContract.Code,
				HousingName = dc.HousingContract.HousingName,
				ResponsableFullName = dc.HousingContract.Responsable != null ? dc.HousingContract.Responsable.FullName : null,
				PilgrimsNumber = dc.NumberOfPilgrims == 0 ? (((dc.Quantity * 40) / 3) / (dc.NumberOfDays == 0 ? 1 : dc.NumberOfDays)) : dc.NumberOfPilgrims,
				Quantity = dc.Quantity,
				DistributionCode = dc.DistributionCode,
				Cumulative = context.DistributionCycleHousingContracts.Where(x => x.HousingContractId == dc.HousingContractId).Sum(hc => hc.Quantity - hc.ReturnedQuantity),
			});
			return this.Paginate(q, filter.page, filter.take);
		}
	}

	public interface IDistributionCycleRepository : IRepository<DistributionCycle>
	{
		public ResultApiModel<IEnumerable<DistributionCycleModel>> GetWithPagination(DistributionCycleFilterModel filter, ApplicationUser user);
		DistributionCycle GetDetailsById(int id);
		string GenerateDistributionCycleNumber();
		QuantitiesModel GetQuantities(DateTime date);
		StatisticsModel GetStatistics();
		ResultApiModel<IEnumerable<DistributionCycleForReportsModel>> GetForReportsWithPagination(DistributionCycleFilterModel filter, ApplicationUser user);
	}
}