using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Zamazimah.Core.Constants;
using Zamazimah.Data.OracleDbConnection;
using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Entities.Identity;
using Zamazimah.Generic.Models;
using Zamazimah.Helpers;
using Zamazimah.Models.HousingContracts;

namespace Zamazimah.Services
{
    public class HousingContractService : IHousingContractService
    {

        private readonly IHousingContractRepository _housingContractRepository;
        private readonly IDistributionCycleRepository _distributionCycleRepository;
        private readonly IPilgrimsTripRepository _pilgrimsTripRepository;

        public HousingContractService(IHousingContractRepository housingContractRepository,
            IDistributionCycleRepository distributionCycleRepository,
            IPilgrimsTripRepository pilgrimsTripRepository)
        {
            _housingContractRepository = housingContractRepository;
            _distributionCycleRepository = distributionCycleRepository;
            _pilgrimsTripRepository = pilgrimsTripRepository;
        }

        public ResultApiModel<IEnumerable<HousingContractModel>> GetWithPagination(HousingContractFilterModel filter)
        {
            return this._housingContractRepository.GetWithPagination(filter);
        }
        public HousingContract GetById(int id)
        {
            return _housingContractRepository.GetById(id);
        }

        public HousingContract GetDetailsById(int id, string base_url)
        {
            var contract = _housingContractRepository.GetDetailsById(id);
            contract.ImageUrl = base_url + contract.ImageUrl;
            return contract;
        }

        public int Create(CreateHousingContractModel model, IFormFile? picture)
        {
            var housingContract = new HousingContract
            {
                Code = model.Code,
                HousingName = model.HousingName,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Location = model.Location,
                PilgrimsNumber = model.PilgrimsNumber,
                LocationNatureId = model.LocationNatureId,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                ResponsableId = model.ResponsableId,
                ResidencePermitNumber = model.ResidencePermitNumber,
                CityId = model.CityId,
                WassilNumber = model.WassilNumber,
                CommercialHousingName = model.CommercialHousingName,
                CenterId = model.CenterId,
            };
            housingContract.ImageUrl = UploadFilesHelper.UploadPicture(picture, PicturesFolder.HousingContractsFolder);

            _housingContractRepository.Insert(housingContract);
            _housingContractRepository.SaveChanges();
            return housingContract.Id;
        }

        public void Update(HousingContract oldModel, CreateHousingContractModel newModel)
        {
            oldModel.Code = newModel.Code;
            oldModel.HousingName = newModel.HousingName;
            oldModel.StartDate = newModel.StartDate;
            oldModel.EndDate = newModel.EndDate;
            oldModel.Location = newModel.Location;
            oldModel.PilgrimsNumber = newModel.PilgrimsNumber;
            oldModel.LocationNatureId = newModel.LocationNatureId;
            oldModel.Latitude = newModel.Latitude;
            oldModel.Longitude = newModel.Longitude;
            oldModel.ResponsableId = newModel.ResponsableId;
            oldModel.ResidencePermitNumber = newModel.ResidencePermitNumber;
            oldModel.CityId = newModel.CityId;
            oldModel.WassilNumber = newModel.WassilNumber;
            oldModel.CommercialHousingName = newModel.CommercialHousingName;
            oldModel.CenterId = newModel.CenterId;
            _housingContractRepository.SaveChanges();
        }

        public int Delete(HousingContract housingContract)
        {
            if (
                !housingContract.DistributionCycleHousingContracts.Any() &&
                !_distributionCycleRepository.IsExist(x => x.DistributionCycleHousingContracts.Any(f => f.HousingContractId == housingContract.Id))
                &&
                !_pilgrimsTripRepository.IsExist(x => x.HousingContractId == housingContract.Id)
                )
            {
                _housingContractRepository.Remove(housingContract);
                return _housingContractRepository.SaveChanges();
            }
            else
            {
                return -1;
            }
        }
        public List<HousingContractAutocompleteModel> AutoComplete(ApplicationUser user, string? query, DateTime? distributionDate = null)
        {
            var results = _housingContractRepository.Get(x => (x.HousingName.Contains(query)
            || x.Location.Contains(query)
            || x.Code.Contains(query)
            || query == null
            || query == string.Empty)
            &&
            (
                (distributionDate != null && !x.DistributionCycleHousingContracts.Any(d => d.DistributionCycle.DistributionDate.Date == distributionDate.Value.Date))
                ||
                distributionDate == null
            )
            ).Take(100).Select(hc => new HousingContractAutocompleteModel
            {
                Id = hc.Id,
                Text = hc.Code + " - " + hc.HousingName,
                HousingName = hc.HousingName,
                Code = hc.Code,
                PilgrimsNumber = hc.PilgrimsNumber,
                Location = hc.Location,
                Latitude = hc.Latitude,
                Longitude = hc.Longitude,
                StartDate = hc.StartDate,
                EndDate = hc.EndDate,
                Created = hc.Created,
                Modified = hc.Modified,
                LocationNatureId = hc.LocationNatureId,
                SuggestedQuantityToDistribue = this.GenerateSuggestedQuantityToDistribue(hc.PilgrimsNumber),
                Order = user.CenterId == hc.CenterId ? 1 : 2,
            }).OrderBy(x => x.Order).ToList();
            return results;
        }

        private int GenerateSuggestedQuantityToDistribue(int pilgrimsNumber)
        {
            return (int)Math.Ceiling(((decimal)pilgrimsNumber * 3) / 40);
        }


        public ImportResult ImportFromExcel(IFormFile file)
        {
            try
            {
                var filepath = UploadFilesHelper.UploadExcelFiles(file);
                int rowno = 0;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                var contracts = new List<HousingContract>();
                foreach (var row in rows)
                {
                    if (rowno > 4)
                    {
                        int number_of_pilgrims = 0;
                        var number = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(number) || string.IsNullOrEmpty(number))
                        {
                            break;
                        }
                        var name = row.Cell(2).Value.ToString();
                        var code = row.Cell(3).Value.ToString();
                        var address = row.Cell(4).Value.ToString();
                        int.TryParse(row.Cell(5).Value.ToString(), out number_of_pilgrims);
                        var existedContract = _housingContractRepository.Get(x => x.Code == code).FirstOrDefault();
                        if (existedContract == null)
                        {
                            var contract = new HousingContract
                            {
                                Code = code,
                                HousingName = name,
                                StartDate = null,
                                EndDate = null,
                                Location = address,
                                PilgrimsNumber = number_of_pilgrims,
                                LocationNatureId = null,
                                ResponsableId = null,
                            };
                            contracts.Add(contract);
                        }
                        else
                        {
                            existedContract.HousingName = name;
                            existedContract.Location = address;
                            existedContract.PilgrimsNumber = number_of_pilgrims;
                        }
                    }
                    rowno++;
                }
                _housingContractRepository.InsertMultiple(contracts);
                _housingContractRepository.SaveChanges();
                return new ImportResult { success = true, description = "success" };
            }
            catch (Exception ex)
            {
                return new ImportResult { success = false, description = ex.Message + ex.InnerException + ex.StackTrace };
            }
        }

        public int ImportFromZamazimahOracleDB()
        {
            IEnumerable<OracleHouseContract> houses = OracleConnectionUtility.GetOracleHouseContracts();
            IEnumerable<HousingContract> db_houses = _housingContractRepository.GetAll();
            var new_house_contracts = new List<HousingContract>();
            foreach (var item in houses)
            {
                var houses_exists_in_db = db_houses.Where(d => d.ResidencePermitNumber == item.HOUSE_REG_NO);
                if (houses_exists_in_db == null || houses_exists_in_db.Count() == 0)
                {
                    new_house_contracts.Add(new HousingContract
                    {
                        Code = item.HOUSE_REG_NO,
                        ResidencePermitNumber = item.HOUSE_REG_NO,
                        HousingName = item.HOUSE_COMMERCIAL_NAME_AR,
                        WassilNumber = item.HOUSE_WASEL_BUILDING_NO + "-" + item.HOUSE_WASEL_POSTAL_CODE + "-" + item.HOUSE_WASEL_ADDITIONAL_NO,
                        Location = item.link,
                        PilgrimsNumber = item.الاستيعاب,
                        StartDate = new DateTime(2022, 6, 15),
                        EndDate = new DateTime(2022, 7, 29),
                        IsImportedFromZamazimahDB = true,
                    });
                }
            }
            _housingContractRepository.InsertMultiple(new_house_contracts);
            return _housingContractRepository.SaveChanges();
        }

    }

    public interface IHousingContractService
    {
        ResultApiModel<IEnumerable<HousingContractModel>> GetWithPagination(HousingContractFilterModel filter);
        HousingContract GetById(int id);
        HousingContract GetDetailsById(int id, string base_url);
        int Create(CreateHousingContractModel model, IFormFile? picture);
        void Update(HousingContract oldModel, CreateHousingContractModel newModel);
        int Delete(HousingContract housingContract);
        List<HousingContractAutocompleteModel> AutoComplete(ApplicationUser user, string? query, DateTime? distributionDate = null);
        ImportResult ImportFromExcel(IFormFile file);
        int ImportFromZamazimahOracleDB();
    }
}
