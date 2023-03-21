using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.DistributorInventory;

namespace Zamazimah.Services
{
    public class DistributorInventoryService : IDistributorInventoryService
    {

        private readonly IDistributorInventoryRepository _distributorInventoryRepository;

        public DistributorInventoryService(IDistributorInventoryRepository DistributorInventoryRepository)
        {
            _distributorInventoryRepository = DistributorInventoryRepository;
        }

        public List<DistributorInventory> GetAll(string userId)
        {
            return _distributorInventoryRepository.Get(x => x.DistributorId == userId).ToList();
        }

        public DistributorInventory GetById(int id)
        {
            return _distributorInventoryRepository.GetById(id);
        }

        public int AddQuantityToInventory(AddDistributorInventoryModel model)
        {
            var DistributorInventory = new DistributorInventory
            {
                DistributorId = model.DistributorId,
                AddedQuantity = model.AddedQuantity,
            };
            _distributorInventoryRepository.Insert(DistributorInventory);
            _distributorInventoryRepository.SaveChanges();
            return DistributorInventory.Id;
        }

        public void Delete(DistributorInventory DistributorInventory)
        {
            _distributorInventoryRepository.Remove(DistributorInventory);
            _distributorInventoryRepository.SaveChanges();
        }

        public void EmptifyDistributorInventory(string id)
        {
            var inventory = _distributorInventoryRepository.GetCurrentDistributorInventory(id);
            if (inventory > 0)
            {
                var di = new DistributorInventory
                {
                    DistributorId = id,
                    IsReturn = true,
                    ConsumedQuantity = inventory,
                };
                _distributorInventoryRepository.Insert(di);
                _distributorInventoryRepository.SaveChanges();
            }
        }

    }

    public interface IDistributorInventoryService
    {
        DistributorInventory GetById(int id);
        List<DistributorInventory> GetAll(string userId);
        int AddQuantityToInventory(AddDistributorInventoryModel model);
        void Delete(DistributorInventory DistributorInventory);
        void EmptifyDistributorInventory(string id);
    }
}
