using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities;
using Zamazimah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zamazimah.Data.Repositories
{
    public class DistributorInventoryRepository : BaseRepository<DistributorInventory>, IDistributorInventoryRepository
    {
        public DistributorInventoryRepository(ZamazimahContext context) : base(context) { }

        public int GetCurrentDistributorInventory(string userId)
        {
            return dbSet.Where(x => x.DistributorId == userId).Sum(d => d.AddedQuantity - d.ConsumedQuantity);
        }
    }
    public interface IDistributorInventoryRepository : IRepository<DistributorInventory>
    {
        int GetCurrentDistributorInventory(string userId);
    }
}
