using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Models.Reports;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ReportTypeController : BaseController
	{

		private IReportTypeService _reportTypeService;


		public ReportTypeController(IReportTypeService reportTypeService, IUserService userService) : base(userService)
		{
			_reportTypeService = reportTypeService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var ReportTypes = _reportTypeService.GetReportTypes();
			return Ok(ReportTypes);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var ReportType = _reportTypeService.GetById(id);
			return Ok(ReportType);
		}

		[HttpPost]
		public IActionResult Create(CreateReportTypeModel model)
		{
			return Ok(new { success = true, id = _reportTypeService.Create(model) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreateReportTypeModel model)
		{
			var ReportType = _reportTypeService.GetById(id);
			if (ReportType == null)
				return NotFound();
			_reportTypeService.Update(ReportType, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var ReportType = _reportTypeService.GetById(id);
			if (ReportType == null)
				return NotFound();
			_reportTypeService.Delete(ReportType);
			return Ok(new { success = true });
		}

	}
}
