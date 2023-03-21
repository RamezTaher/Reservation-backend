using Microsoft.AspNetCore.Http;
using Zamazimah.Core.Constants;
using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Entities.Identity;
using Zamazimah.Generic.Models;
using Zamazimah.Helpers;
using Zamazimah.Models.Reports;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Services
{
    public class ReportService : IReportService
    {

        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public ResultApiModel<IEnumerable<ReportModel>> GetWithPagination(ReportFilter filter)
        {
            return this._reportRepository.GetWithPagination(filter);
        }
        public Report GetById(int id)
        {
            return _reportRepository.GetById(id);
        }

        public Report GetDetailsById(int id)
        {
            return _reportRepository.GetDetailsById(id);
        }

        public int Create(CreateReportModel model, ApplicationUser user, IFormFile? file = null)
        {

            var report = new Report
            {
                Title = model.Title,
                Text  = model.Text,
                ReportTypeId =  model.ReportTypeId,
                UserId = user.Id,
                Status = ReportStatus.New,
                Priority = model.Priority,
            };

            report.FileUrl = UploadFilesHelper.UploadPicture(file, PicturesFolder.HousingContractsFolder);
            _reportRepository.Insert(report);
            _reportRepository.SaveChanges();
            return report.Id;
        }

        public void Update(Report oldReport, CreateReportModel model)
        {
            oldReport.Title = model.Title;
            oldReport.Text = model.Text;
            oldReport.ReportTypeId = model.ReportTypeId;
            _reportRepository.SaveChanges();
        }

        public void CloseReport(Report oldReport)
        {
            oldReport.Status = ReportStatus.Closed;
            oldReport.ClosingDate = DateTime.Now;
            _reportRepository.SaveChanges();
        }

        public void Delete(Report Report)
        {
            _reportRepository.Remove(Report);
            _reportRepository.SaveChanges();
        }

    }

    public interface IReportService
    {
        ResultApiModel<IEnumerable<ReportModel>> GetWithPagination(ReportFilter filter);
        Report GetById(int id);
        int Create(CreateReportModel model, ApplicationUser user, IFormFile? file = null);
        void Update(Report oldReport, CreateReportModel newModel);
        public void Delete(Report Report);
        Report GetDetailsById(int id);
        void CloseReport(Report oldReport);
    }
}
