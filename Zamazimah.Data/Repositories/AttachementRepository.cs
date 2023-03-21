using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;

namespace Zamazimah.Data.Repositories
{
    public class AttachementRepository : BaseRepository<Attachement>, IAttachementRepository
	{
		public AttachementRepository(ZamazimahContext context) : base(context)
		{
		}
	}

	public interface IAttachementRepository : IRepository<Attachement>
	{
	}
}