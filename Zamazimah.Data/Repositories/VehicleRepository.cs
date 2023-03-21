using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.Vehicles;

namespace Zamazimah.Data.Repositories
{
	public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
	{
		public VehicleRepository(ZamazimahContext context) : base(context)
		{
		}

		public ResultApiModel<IEnumerable<VehicleModel>> GetWithPagination(VehicleFilterModel filter)
		{
			var q = dbSet.Where(hc => 
			(hc.PlateNumber.Contains(filter.PlateNumber) || filter.PlateNumber == null || filter.PlateNumber == String.Empty)
			&&
			(hc.FormNumber.Contains(filter.FormNumber) || filter.FormNumber == null || filter.FormNumber == String.Empty)
			&&
			(hc.FormExpiration.Date == filter.FormExpiration || filter.FormExpiration == null)
			&&
			(hc.Brand.Contains(filter.Brand) || filter.Brand == null || filter.Brand == String.Empty)
			&&
			(hc.InsuranceNumber.Contains(filter.InsuranceNumber) || filter.InsuranceNumber == null || filter.InsuranceNumber == String.Empty)
			&&
			(hc.InsuranceExpiration.Date == filter.InsuranceExpiration || filter.InsuranceExpiration == null)
			&&
			(hc.Age == filter.Age || filter.Age == null)
			&&
			(hc.MaxLoad == filter.MaxLoad || filter.MaxLoad == null)
			).Select(hc => new VehicleModel
			{
				Id = hc.Id,
				Age = hc.Age,
				Brand = hc.Brand,
				FormExpiration = hc.FormExpiration,
				FormNumber = hc.FormNumber,
				InsuranceExpiration = hc.InsuranceExpiration,
				InsuranceNumber = hc.InsuranceNumber,
				MaxLoad = hc.MaxLoad,
				PlateNumber = hc.PlateNumber
			});
			return this.Paginate(q, filter.page, filter.take);
		}

	}

	public interface IVehicleRepository : IRepository<Vehicle>
	{
		ResultApiModel<IEnumerable<VehicleModel>> GetWithPagination(VehicleFilterModel filter);
	}
}