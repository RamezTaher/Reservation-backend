using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.DistributionContracts;

namespace Zamazimah.Services
{
    public class DistributionContractService : IDistributionContractService
    {

        private readonly IDistributionContractRepository _distributionContractRepository;

        public DistributionContractService(IDistributionContractRepository DistributionContractRepository)
        {
            _distributionContractRepository = DistributionContractRepository;
        }

        public ResultApiModel<IEnumerable<DistributionContractModel>> GetWithPagination(DistributionContractFilterModel filter)
        {
            return this._distributionContractRepository.GetWithPagination(filter);
        }
        public DistributionContract GetById(int id)
        {
            return _distributionContractRepository.GetById(id);
        }

        public int Create(CreateDistributionContractModel model)
        {
            var DistributionContract = new DistributionContract
            {
                NumberOfVehicles = model.NumberOfVehicles,
                NumberOfDrivers = model.NumberOfDrivers,
                DistributionCompanyName = model.DistributionCompanyName,
                Code = model.Code,
                GregorianEndDate = model.GregorianEndDate,
                GregorianStartDate = model.GregorianStartDate,
                HijrieEndDate = model.HijrieEndDate,
                HijriStartDate = model.HijriStartDate,
                SignatureDate = model.SignatureDate,
            };
            _distributionContractRepository.Insert(DistributionContract);
            _distributionContractRepository.SaveChanges();
            return DistributionContract.Id;
        }

        public void Update(DistributionContract oldDistributionContract, CreateDistributionContractModel model)
        {
            oldDistributionContract.NumberOfVehicles = model.NumberOfVehicles;
            oldDistributionContract.NumberOfDrivers = model.NumberOfDrivers;
            oldDistributionContract.DistributionCompanyName = model.DistributionCompanyName;
            oldDistributionContract.Code = model.Code;
            oldDistributionContract.GregorianEndDate = model.GregorianEndDate;
            oldDistributionContract.GregorianStartDate = model.GregorianStartDate;
            oldDistributionContract.HijrieEndDate = model.HijrieEndDate;
            oldDistributionContract.HijriStartDate = model.HijriStartDate;
            oldDistributionContract.SignatureDate = model.SignatureDate;
            _distributionContractRepository.SaveChanges();
        }

        public void Delete(DistributionContract DistributionContract)
        {
            _distributionContractRepository.Remove(DistributionContract);
            _distributionContractRepository.SaveChanges();
        }

        public List<DistributionContractModel> AutoComplete(string? query)
        {
            var results = _distributionContractRepository.Get(x => x.Code.Contains(query)
            || x.DistributionCompanyName.Contains(query)
            || query == null
            || query == String.Empty
            ).Take(100).Select(hc => new DistributionContractModel
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
            }).ToList();
            return results;
        }

    }

    public interface IDistributionContractService
    {
        ResultApiModel<IEnumerable<DistributionContractModel>> GetWithPagination(DistributionContractFilterModel filter);
        DistributionContract GetById(int id);
        int Create(CreateDistributionContractModel model);
        void Update(DistributionContract oldDistributionContract, CreateDistributionContractModel newModel);
        public void Delete(DistributionContract DistributionContract);
        List<DistributionContractModel> AutoComplete(string? query);
    }
}
