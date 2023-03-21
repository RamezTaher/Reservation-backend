using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.Stores;

namespace Zamazimah.Data.Repositories
{
	public class StoreRepository : BaseRepository<Store>, IStoreRepository
	{
		public StoreRepository(ZamazimahContext context) : base(context)
		{
		}

		public ResultApiModel<IEnumerable<StoreModel>> GetWithPagination(StoreFilterModel filter)
		{
			var q = dbSet.Where(hc => 
			(hc.StoreNumber.Contains(filter.StoreNumber) || filter.StoreNumber == null || filter.StoreNumber == String.Empty)
			&&
			(hc.StoreNumber.Contains(filter.Address) || filter.Address == null || filter.Address == String.Empty)
			&&
			( hc.ResponsableFirstName.Contains(filter.Responsable) 
			||
			hc.ResponsableLastName.Contains(filter.Responsable)
			||
			hc.ResponsableEmail.Contains(filter.Responsable)
			||
			(hc.ResponsablePhone.Contains(filter.Responsable)
			|| 
			filter.Responsable == null 
			||
			filter.Responsable == String.Empty)
			)
			&&
			(hc.Capacity == filter.Capacity || filter.Capacity == null)
			&&
			(hc.AlertQuantity == filter.AlertQuantity || filter.AlertQuantity == null)
			).Select(hc => new StoreModel
			{
				Id = hc.Id,
				StoreNumber = hc.StoreNumber,
				Title = hc.Title,
				AvailableQuantity = hc.Capacity - hc.DistributionCycles.Sum(s => s.DistributionCycleHousingContracts.Sum(d => d.Quantity)),
				ReservedQuantity = hc.DistributionCycles.Sum(s=>s.DistributionCycleHousingContracts.Sum(d=>d.Quantity)),
				ActualQuantity = hc.Capacity - hc.DistributionCycles.Sum(s => s.DistributionCycleHousingContracts.Sum(d => d.Quantity)),
				Address = hc.Address,
				AlertQuantity = hc.AlertQuantity,
				Capacity = hc.Capacity,
				ResponsableEmail = hc.ResponsableEmail,
				ResponsableFirstName = hc.ResponsableFirstName,
				ResponsableLastName = hc.ResponsableLastName,
				ResponsablePhone = hc.ResponsablePhone,
				Created = hc.Created,
				Modified = hc.Modified,
			});
			return this.Paginate(q, filter.page, filter.take);
		}

	}

	public interface IStoreRepository : IRepository<Store>
	{
		public ResultApiModel<IEnumerable<StoreModel>> GetWithPagination(StoreFilterModel filter);
	}
}