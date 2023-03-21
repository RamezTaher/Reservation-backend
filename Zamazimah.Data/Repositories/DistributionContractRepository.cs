using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.DistributionContracts;

namespace Zamazimah.Data.Repositories
{
	public class DistributionContractRepository : BaseRepository<DistributionContract>, IDistributionContractRepository
	{
		public DistributionContractRepository(ZamazimahContext context) : base(context)
		{
		}

		public ResultApiModel<IEnumerable<DistributionContractModel>> GetWithPagination(DistributionContractFilterModel filter)
		{
			var q = dbSet.Where(hc => 
			(hc.Code.Contains(filter.Code) || filter.Code == null || filter.Code == String.Empty)
			&&
			(hc.DistributionCompanyName.Contains(filter.DistributionCompanyName) || filter.DistributionCompanyName == null || filter.DistributionCompanyName == String.Empty)
			&&
			(hc.SignatureDate.Date == filter.SignatureDate || filter.SignatureDate == null)
			&&
			(hc.GregorianStartDate.Date == filter.GregorianStartDate || filter.GregorianStartDate == null)
			&&
			(hc.GregorianEndDate.Date == filter.GregorianEndDate || filter.GregorianEndDate == null)
			&&
			(hc.NumberOfDrivers == filter.NumberOfDrivers || filter.NumberOfDrivers == null)
			&&
			(hc.NumberOfVehicles == filter.NumberOfVehicles || filter.NumberOfVehicles == null)
			).Select(hc => new DistributionContractModel
			{
				Id = hc.Id,
				Code = hc.Code,
				Created = hc.Created,
				DistributionCompanyName = hc.DistributionCompanyName,
				GregorianEndDate = hc.GregorianEndDate,
				GregorianStartDate = hc.GregorianStartDate,
				HijrieEndDate = hc.HijrieEndDate,
				HijriStartDate = hc.HijriStartDate,
				Modified = hc.Modified,
				NumberOfDrivers = hc.NumberOfDrivers,
				NumberOfVehicles = hc.NumberOfVehicles,
				SignatureDate = hc.SignatureDate,
			});
			return this.Paginate(q, filter.page, filter.take);
		}

	}

	public interface IDistributionContractRepository : IRepository<DistributionContract>
	{
		public ResultApiModel<IEnumerable<DistributionContractModel>> GetWithPagination(DistributionContractFilterModel filter);
	}
}