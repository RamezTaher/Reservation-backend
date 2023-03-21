using Microsoft.AspNetCore.Http;
using System.Globalization;
using Zamazimah.Core.Constants;
using Zamazimah.Data.OracleDbConnection;
using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Entities.Identity;
using Zamazimah.Generic.Models;
using Zamazimah.Helpers;
using Zamazimah.Models.PilgrimsTrips;

namespace Zamazimah.Services
{
    public class PilgrimsTripService : IPilgrimsTripService
    {

        private readonly IPilgrimsTripRepository _pilgrimsTripRepository;
        private IDistributorInventoryRepository _distributorInventoryRepository;
        private ITransportCompanyRepository _transportCompanyRepository;

        public PilgrimsTripService(IPilgrimsTripRepository pilgrimsTripRepository,
            IDistributorInventoryRepository distributorInventoryRepository,
            ITransportCompanyRepository transportCompanyRepository)
        {
            _pilgrimsTripRepository = pilgrimsTripRepository;
            _distributorInventoryRepository = distributorInventoryRepository;
            _transportCompanyRepository = transportCompanyRepository;
        }

        public ResultApiModel<IEnumerable<PilgrimsTripModel>> GetWithPagination(PilgrimsTripFilterModel filter, ApplicationUser user)
        {
            var list = _pilgrimsTripRepository.GetWithPagination(filter, user);
            list.OtherTotal = _distributorInventoryRepository.GetCurrentDistributorInventory(user.Id);
            return list;
        }
        public PilgrimsTrip GetById(int id)
        {
            return _pilgrimsTripRepository.GetById(id);
        }

        public PilgrimsTrip GetDetailsById(int id)
        {
            return _pilgrimsTripRepository.GetDetailsById(id);
        }

        public int Create(CreatePilgrimsTripModel model)
        {
            var PilgrimsTrip = new PilgrimsTrip
            {
                EntryPoint = model.EntryPoint,
                HousingContractId = model.HousingContractId,
                TripNumber = model.TripNumber,
                TransportCompanyName = model.TransportCompanyName,
                Date = model.Date,
                PilgrimsNumber = model.PilgrimsNumber
            };
            _pilgrimsTripRepository.Insert(PilgrimsTrip);
            _pilgrimsTripRepository.SaveChanges();
            return PilgrimsTrip.Id;
        }

        public int CreateUrgentTrip(CreateUrgentTripPilgrimsTripModel model, ApplicationUser user, IFormFile? picture)
        {
            var PilgrimsTrip = new PilgrimsTrip
            {
                TripNumber = model.TripNumber,
                Date = DateTime.Now,
                PilgrimsNumber = model.PilgrimsNumber,
                Description = model.Description,
                DistributedQuantity = model.DistributedQuantity,
                DistributorId = user.Id,
                DistributionCompletedDate = DateTime.Now,   
                IsDistributionDone = true,
                VehiclePlateNumber = model.VehiclePlateNumber,
                Type = model.Type,
            };
            PilgrimsTrip.TripImage = UploadFilesHelper.UploadPicture(picture, PicturesFolder.HousingContractsFolder);
            _distributorInventoryRepository.Insert(new DistributorInventory
            {
                ConsumedQuantity = model.DistributedQuantity,
                DistributorId = user.Id,
            });
            _pilgrimsTripRepository.Insert(PilgrimsTrip);
            _pilgrimsTripRepository.SaveChanges();
            return PilgrimsTrip.Id;
        }


        public void Update(PilgrimsTrip oldModel, CreatePilgrimsTripModel newModel)
        {
            oldModel.EntryPoint = newModel.EntryPoint;
            oldModel.HousingContractId = newModel.HousingContractId;
            oldModel.TripNumber = newModel.TripNumber;
            oldModel.TransportCompanyName = newModel.TransportCompanyName;
            oldModel.Date = newModel.Date;
            oldModel.PilgrimsNumber = newModel.PilgrimsNumber;
            _pilgrimsTripRepository.SaveChanges();
        }

        public void MarkAsDone(PilgrimsTrip oldModel, PilgrimsTripDistributionCompletedModel newModel, ApplicationUser user)
        {
            oldModel.DistributedQuantity = newModel.DistributedQuantity;
            oldModel.OverDistributionReason = newModel.OverDistributionReason;
            oldModel.IsDistributionDone = true;
            oldModel.DistributionCompletedDate = DateTime.Now;
            oldModel.DistributorId = user.Id;
            _distributorInventoryRepository.Insert(new DistributorInventory
            {
                ConsumedQuantity = newModel.DistributedQuantity,
                DistributorId = user.Id,
            });
            _pilgrimsTripRepository.SaveChanges();
        }

        public void Delete(PilgrimsTrip PilgrimsTrip)
        {
            _pilgrimsTripRepository.Remove(PilgrimsTrip);
            _pilgrimsTripRepository.SaveChanges();
        }
        public List<PilgrimsTripModel> AutoComplete(string query)
        {
            var results = _pilgrimsTripRepository.Get(x => x.TripNumber.Contains(query)
            || x.EntryPoint.Contains(query)
            || x.TransportCompanyName.Contains(query)
            || query == null
            || query == String.Empty
            ).Take(100).Select(hc => new PilgrimsTripModel
            {
                Id = hc.Id,
                TripNumber = hc.TripNumber,
                Date = hc.Date,
                EntryPoint = hc.EntryPoint,
                PilgrimsNumber = hc.PilgrimsNumber,
                TransportCompanyName = hc.TransportCompanyName,
            }).ToList();
            return results;
        }


        public void ImportFromZamazimahOracleDB()
        {
            IEnumerable<OracleTrip> trips = OracleConnectionUtility.GetOracleHajjTrips();
            var listPilgrimTrips = new List<PilgrimsTrip>();
            var our_pilgrims_trips = _pilgrimsTripRepository.Get(x => x.IsImportedFromZamazimahDB).ToList();
            foreach (OracleTrip trip in trips)
            {
                var transportCompany = this.GetTransportCompanyOrCreateIfNotExist(trip.BI_LTC_ID);
                var our_trip = our_pilgrims_trips.Where(t => t.TripNumber == trip.BI_OPERATING_CARD_NO && t.Date.ToString("dd/MM/yyyy") == trip.MANIFEST_TRIP_DATE && (t.TransportCompanyId==null?"": t.TransportCompanyId.ToString()) == trip.BI_LTC_ID).FirstOrDefault();
                if (our_trip == null)
                {
                    listPilgrimTrips.Add(new PilgrimsTrip
                    {
                        PilgrimsNumber = trip.SUM_CUNT,
                        TripNumber = trip.BI_OPERATING_CARD_NO,
                        Date = DateTime.ParseExact(trip.MANIFEST_TRIP_DATE, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        IsImportedFromZamazimahDB = true,
                        TransportCompanyName = "شركة نقل رقم :" + trip.BI_LTC_ID,
                        TransportCompanyId = transportCompany.Id,
                        VehiclePlateNumber = trip.BI_PLATE_NO,
                    });
                }
                else
                {
                    our_trip.PilgrimsNumber = trip.SUM_CUNT;
                    our_trip.Date = DateTime.ParseExact(trip.MANIFEST_TRIP_DATE, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    our_trip.TransportCompanyName = "شركة نقل رقم :" + trip.BI_LTC_ID;
                    our_trip.TransportCompanyId = transportCompany.Id;
                    our_trip.VehiclePlateNumber = trip.BI_PLATE_NO;
                }
            }
            _pilgrimsTripRepository.InsertMultiple(listPilgrimTrips);
            _pilgrimsTripRepository.SaveChanges();
        }

        private TransportCompany GetTransportCompanyOrCreateIfNotExist(string code)
        {
            var transportCompany = _transportCompanyRepository.Get(x => x.Code == code).FirstOrDefault();
            if (transportCompany != null)
            {
                return transportCompany;
            }
            else
            {
                var new_transportCompany = new TransportCompany
                {
                    Id = int.Parse(code),
                    Code = code,
                    Name = "شركة نقل رقم :" + code,
                    NameEn = "شركة نقل رقم :" + code,
                };
                _transportCompanyRepository.Insert(new_transportCompany);
                _transportCompanyRepository.SaveChanges();
                return new_transportCompany;
            }
        }

        public List<DistributorPerformanceModel> GetDistributorsPerformance()
        {
           return _pilgrimsTripRepository.GetDistributorsPerformance();
        }


    }

    public interface IPilgrimsTripService
    {
        ResultApiModel<IEnumerable<PilgrimsTripModel>> GetWithPagination(PilgrimsTripFilterModel filter, ApplicationUser user);
        PilgrimsTrip GetById(int id);
        int Create(CreatePilgrimsTripModel model);
        int CreateUrgentTrip(CreateUrgentTripPilgrimsTripModel model, ApplicationUser user, IFormFile? picture);
        void Update(PilgrimsTrip oldModel, CreatePilgrimsTripModel newModel);
        void Delete(PilgrimsTrip PilgrimsTrip);
        List<PilgrimsTripModel> AutoComplete(string query);
        void MarkAsDone(PilgrimsTrip oldModel, PilgrimsTripDistributionCompletedModel newModel, ApplicationUser user);
        void ImportFromZamazimahOracleDB();
        PilgrimsTrip GetDetailsById(int id);
        List<DistributorPerformanceModel> GetDistributorsPerformance();
    }
}
