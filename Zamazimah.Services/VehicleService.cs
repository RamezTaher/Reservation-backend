using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.Vehicles;

namespace Zamazimah.Services
{
    public class VehicleService : IVehicleService
    {

        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository VehicleRepository)
        {
            _vehicleRepository = VehicleRepository;
        }

        public ResultApiModel<IEnumerable<VehicleModel>> GetWithPagination(VehicleFilterModel filter)
        {
            return this._vehicleRepository.GetWithPagination(filter);
        }
        public Vehicle GetById(int id)
        {
            return _vehicleRepository.GetById(id);
        }

        public int Create(CreateVehicleModel model)
        {
            var Vehicle = new Vehicle
            {
                Age = model.Age,
                Brand = model.Brand,
                FormExpiration = model.FormExpiration,
                FormNumber = model.FormNumber,
                InsuranceExpiration = model.InsuranceExpiration,
                InsuranceNumber = model.InsuranceNumber,
                MaxLoad = model.MaxLoad,
                PlateNumber = model.PlateNumber,
            };
            _vehicleRepository.Insert(Vehicle);
            _vehicleRepository.SaveChanges();
            return Vehicle.Id;
        }

        public void Update(Vehicle oldVehicle, CreateVehicleModel newModel)
        {
            oldVehicle.Age = newModel.Age;
            oldVehicle.Brand = newModel.Brand;
            oldVehicle.FormExpiration = newModel.FormExpiration;
            oldVehicle.FormNumber = newModel.FormNumber;
            oldVehicle.InsuranceExpiration = newModel.InsuranceExpiration;
            oldVehicle.InsuranceNumber = newModel.InsuranceNumber;
            oldVehicle.MaxLoad = newModel.MaxLoad;
            oldVehicle.PlateNumber = newModel.PlateNumber;
            _vehicleRepository.SaveChanges();
        }

        public void Delete(Vehicle Vehicle)
        {
            _vehicleRepository.Remove(Vehicle);
            _vehicleRepository.SaveChanges();
        }

        public List<VehicleModel> AutoComplete(string query)
        {
            var users = _vehicleRepository.Get(x => x.Brand.Contains(query)
            || x.FormNumber.Contains(query)
            || x.PlateNumber.Contains(query)
            || query == null
            || query == String.Empty
            ).Take(100).Select(hc => new VehicleModel
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
            }).ToList();
            return users;
        }

    }

    public interface IVehicleService
    {
        ResultApiModel<IEnumerable<VehicleModel>> GetWithPagination(VehicleFilterModel filter);
        Vehicle GetById(int id);
        int Create(CreateVehicleModel model);
        void Update(Vehicle oldVehicle, CreateVehicleModel newModel);
        public void Delete(Vehicle Vehicle);
        List<VehicleModel> AutoComplete(string query);
    }
}
