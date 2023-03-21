using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;

namespace Zamazimah.Data.Repositories
{
	public class TransportCompanyRepository : BaseRepository<TransportCompany>, ITransportCompanyRepository
	{
		public TransportCompanyRepository(ZamazimahContext context) : base(context)
		{
		}

	}

	public interface ITransportCompanyRepository : IRepository<TransportCompany>
	{
	}
}