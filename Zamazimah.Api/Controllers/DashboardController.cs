using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Models.Vehicles;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DashboardController : BaseController
	{

		private IDashboardService _dashboardService;


		public DashboardController(IDashboardService dashboardService, IUserService userService) : base(userService)
		{
			_dashboardService = dashboardService;
		}


		[HttpGet("quantities")]
		[AllowAnonymous]
		public IActionResult GetQuantitiees(DateTime date)
		{
			var quantities = _dashboardService.GetDistributionQuantity(date);
			return Ok(quantities);
		}

		[HttpGet("statistics")]
		[AllowAnonymous]
		public IActionResult GetStatistics()
		{
			var statistics = _dashboardService.GetStatistics();
			return Ok(statistics);
		}

	}
}
