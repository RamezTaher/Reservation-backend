using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.Stores;

namespace Zamazimah.Services
{
    public class StoreService : IStoreService
    {

        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public ResultApiModel<IEnumerable<StoreModel>> GetWithPagination(StoreFilterModel filter)
        {
            return this._storeRepository.GetWithPagination(filter);
        }
        public Store GetById(int id)
        {
            return _storeRepository.GetById(id);
        }

        public int Create(CreateStoreModel model)
        {
            var Store = new Store
            {
                StoreNumber = model.StoreNumber,
                Address = model.Address,
                AlertQuantity = model.AlertQuantity,
                Capacity = model.Capacity,
                ResponsableEmail = model.ResponsableEmail,
                ResponsableFirstName = model.ResponsableFirstName,
                ResponsableLastName = model.ResponsableLastName,
                ResponsablePhone = model.ResponsablePhone,
                Title = model.Title
            };
            _storeRepository.Insert(Store);
            _storeRepository.SaveChanges();
            return Store.Id;
        }

        public void Update(Store oldStore, CreateStoreModel model)
        {
            oldStore.StoreNumber = model.StoreNumber;
            oldStore.Address = model.Address;
            oldStore.AlertQuantity = model.AlertQuantity;
            oldStore.Capacity = model.Capacity;
            oldStore.ResponsableEmail = model.ResponsableEmail;
            oldStore.ResponsableFirstName = model.ResponsableFirstName;
            oldStore.ResponsableLastName = model.ResponsableLastName;
            oldStore.ResponsablePhone = model.ResponsablePhone;
            oldStore.Title = model.Title;
            _storeRepository.SaveChanges();
        }

        public void Delete(Store Store)
        {
            _storeRepository.Remove(Store);
            _storeRepository.SaveChanges();
        }

        public List<StoreAutocompleteModel> AutoComplete(string? query)
        {
            var users = _storeRepository.Get(x => x.StoreNumber.Contains(query)
            || x.ResponsableFirstName.Contains(query)
            || x.ResponsableLastName.Contains(query)
            || x.Address.Contains(query)
            || query == null
            || query == String.Empty
            ).Take(100).Select(hc => new StoreAutocompleteModel
            {
                Id = hc.Id,
                Text = hc.StoreNumber + " - " + hc.Title,
                StoreNumber = hc.StoreNumber,
                Address = hc.Address,
                AlertQuantity = hc.AlertQuantity,
                Capacity = hc.Capacity,
                ResponsableEmail = hc.ResponsableEmail,
                ResponsableFirstName = hc.ResponsableFirstName,
                ResponsableLastName = hc.ResponsableLastName,
                ResponsablePhone = hc.ResponsablePhone,
                Created = hc.Created,
                Modified = hc.Modified
            }).ToList();
            return users;
        }

    }

    public interface IStoreService
    {
        ResultApiModel<IEnumerable<StoreModel>> GetWithPagination(StoreFilterModel filter);
        Store GetById(int id);
        int Create(CreateStoreModel model);
        void Update(Store oldStore, CreateStoreModel newModel);
        public void Delete(Store Store);
        List<StoreAutocompleteModel> AutoComplete(string? query);
    }
}
