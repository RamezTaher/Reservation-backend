using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Models.Reports;

namespace Zamazimah.Services
{
    public class ReportTypeService : IReportTypeService
    {

        private readonly IReportTypeRepository _ReportTypeRepository;

        public ReportTypeService(IReportTypeRepository ReportTypeRepository)
        {
            _ReportTypeRepository = ReportTypeRepository;
        }

        public List<ReportTypeModel> GetReportTypes()
        {
            var ReportTypes = this._ReportTypeRepository.GetAll();
            var results = ReportTypes.Select(x => new ReportTypeModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return results;
        }
        public ReportType GetById(int id)
        {
            return _ReportTypeRepository.GetById(id);
        }

        public int Create(CreateReportTypeModel model)
        {
            var ReportType = new ReportType
            {
                Name = model.Name,
            };
            _ReportTypeRepository.Insert(ReportType);
            _ReportTypeRepository.SaveChanges();
            return ReportType.Id;
        }

        public void Update(ReportType oldReportType, CreateReportTypeModel model)
        {
            oldReportType.Name = model.Name;
            _ReportTypeRepository.SaveChanges();
        }

        public void Delete(ReportType ReportType)
        {
            _ReportTypeRepository.Remove(ReportType);
            _ReportTypeRepository.SaveChanges();
        }
    }

    public interface IReportTypeService
    {
        ReportType GetById(int id);
        int Create(CreateReportTypeModel model);
        void Update(ReportType oldReportType, CreateReportTypeModel newModel);
        public void Delete(ReportType ReportType);
        List<ReportTypeModel> GetReportTypes();
    }
}
