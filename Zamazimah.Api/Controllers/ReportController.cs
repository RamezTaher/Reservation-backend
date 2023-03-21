using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Api.ModelBinders;
using Zamazimah.Models.Reports;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ReportController : BaseController
	{

		private IReportService _reportService;
		public ReportController(IReportService reportService, IUserService userService) : base(userService)
		{
			_reportService = reportService;
		}

		[HttpGet]
		public IActionResult Get([FromQuery]ReportFilter filter)
		{
			var Reports = _reportService.GetWithPagination(filter);
			return Ok(Reports);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var Report = _reportService.GetDetailsById(id);
			return Ok(Report);
		}

		[HttpPost]
		public IActionResult Create([ModelBinder(BinderType = typeof(JsonModelBinder))] CreateReportModel model, IFormFile? file = null)
		{
			return Ok(new { success = true, id = _reportService.Create(model, _user, file) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreateReportModel model)
		{
			var Report = _reportService.GetById(id);
			if (Report == null)
				return NotFound();
			_reportService.Update(Report, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var Report = _reportService.GetById(id);
			if (Report == null)
				return NotFound();
			_reportService.Delete(Report);
			return Ok(new { success = true });
		}

		[HttpPut("close_report/{id}")]
		public IActionResult CloseReport(int id)
		{
			var Report = _reportService.GetById(id);
			if (Report == null)
				return NotFound();
			_reportService.CloseReport(Report);
			return Ok(new { success = true });
		}

	}
}
