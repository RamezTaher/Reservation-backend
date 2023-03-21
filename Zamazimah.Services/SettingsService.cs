using Zamazimah.Core.Constants;
using Zamazimah.Data.Repositories;
using Zamazimah.Entities;

namespace Zamazimah.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICenterRepository _centerRepository;
        private readonly ILocationNatureRepository _locationNatureRepository;
        private readonly IDistributionPointRepository _distributionPointRepository;
        private readonly ITransportCompanyRepository _transportCompanyRepository;
        public SettingsService(ICountryRepository countryRepository,
            ILocationNatureRepository locationNatureRepository,
            ICityRepository cityRepository,
            ICenterRepository centerRepository,
            IDistributionPointRepository distributionPointRepository,
            ITransportCompanyRepository transportCompanyRepository)
        {
            _countryRepository = countryRepository;
            _locationNatureRepository = locationNatureRepository;
            _distributionPointRepository = distributionPointRepository;
            _cityRepository = cityRepository;
            _transportCompanyRepository = transportCompanyRepository;
            _centerRepository = centerRepository;
        }

        public List<Country> GetAllCountries(string lang = Langs.AR)
        {
            var countries = _countryRepository.GetAll().ToList();
            countries.ForEach(x =>
            {
                x.Name = lang == Langs.AR ? x.NameAR : x.Name;
            });
            if (lang == Langs.AR)
            {
                countries = countries.OrderBy(d => d.NameAR).ToList();
            }
            else
            {
                countries = countries.OrderBy(d => d.Name).ToList();
            }
            return countries;
        }

        public List<City> GetAllCities(string lang = Langs.AR)
        {
            var cities = _cityRepository.GetAll().ToList();
            return cities;
        }

        public List<LocationNature> GetAllLocationNatures(string lang = Langs.AR)
        {
            var results = _locationNatureRepository.GetAll().ToList();
            return results;
        }

        public List<DistributionPoint> GetAllDistributionPoints(string lang = Langs.AR)
        {
            var results = _distributionPointRepository.GetAll().ToList();
            return results;
        }

        public List<Center> GetAllCenters(string lang = Langs.AR)
        {
            var results = _centerRepository.GetAll().ToList();
            return results;
        }

        public List<TransportCompany> GetTransportCompaniesAutocomplete(string? query = null, string lang = Langs.AR)
        {
            var results = _transportCompanyRepository.Get(x=>x.Name.Contains(query) 
            || (x.NameEn != null && x.NameEn.Contains(query)) 
            || query == null
            || query == string.Empty).ToList();
            return results;
        }

    }

    public interface ISettingsService
    {
        List<Country> GetAllCountries(string lang = Langs.AR);
        List<LocationNature> GetAllLocationNatures(string lang = Langs.AR);
        List<DistributionPoint> GetAllDistributionPoints(string lang = Langs.AR);
        List<City> GetAllCities(string lang = Langs.AR);
        List<TransportCompany> GetTransportCompaniesAutocomplete(string? query = null, string lang = Langs.AR);
        List<Center> GetAllCenters(string lang = Langs.AR);
    }
}
