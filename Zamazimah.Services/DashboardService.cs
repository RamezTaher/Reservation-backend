using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.Dashboard;
using Zamazimah.Models.Vehicles;

namespace Zamazimah.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IDistributionCycleRepository _distributionCycleRepository;

        public DashboardService(IDistributionCycleRepository distributionCycleRepository)
        {
            _distributionCycleRepository = distributionCycleRepository;
        }

        public QuantitiesModel GetDistributionQuantity(DateTime date)
        {
            return _distributionCycleRepository.GetQuantities(date);
        }

        public StatisticsModel GetStatistics()
        {
            return _distributionCycleRepository.GetStatistics();
        }

    }

    public interface IDashboardService
    {
        QuantitiesModel GetDistributionQuantity(DateTime date);
        StatisticsModel GetStatistics();
    }



}
