using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Models.DistributionCycles;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DistributionCycleController : BaseController
	{

		private IDistributionCycleService _distributionCycleService;
		private readonly IConfiguration _configuration;
		private string base_url;

		public DistributionCycleController(IDistributionCycleService distributionCycleService,
			IUserService userService,
			IConfiguration configuration) : base(userService)
		{
			_configuration = configuration;
			_distributionCycleService = distributionCycleService;
			this.base_url = _configuration["BASE_URL"];
		}

		[HttpGet]
		public IActionResult Get([FromQuery]DistributionCycleFilterModel filter)
		{
			var DistributionCycles = _distributionCycleService.GetWithPagination(filter, _user);
			return Ok(DistributionCycles);
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var DistributionCycle = _distributionCycleService.GetDetailsById(id);
			if (DistributionCycle == null)
				return NotFound();
			DistributionCycle.DistributionCycleHousingContracts.ToList().ForEach(x =>
			{
				x.DistributionCycle = null;
				x.HousingContract.DistributionCycleHousingContracts = null;
			});
			DistributionCycle.Store.DistributionCycles = null;
			DistributionCycle.DistributionImageUrl = DistributionCycle.DistributionImageUrl != null ? this.base_url + DistributionCycle.DistributionImageUrl : null;
			return Ok(DistributionCycle);
		}

		[HttpPost]
		public IActionResult Create(CreateDistributionCycleModel model)
		{
			return Ok(new { success = true, id = _distributionCycleService.Create(model) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreateDistributionCycleModel model)
		{
			var DistributionCycle = _distributionCycleService.GetById(id);
			if (DistributionCycle == null)
				return NotFound();
			_distributionCycleService.Update(DistributionCycle, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var DistributionCycle = _distributionCycleService.GetById(id);
			if (DistributionCycle == null)
				return NotFound();
			_distributionCycleService.Delete(DistributionCycle);
			return Ok(new { success = true });
		}

		[HttpPut("start/{id}")]
		public IActionResult Start(int id)
		{
			var DistributionCycle = _distributionCycleService.GetDetailsById(id);
			if (DistributionCycle == null)
				return NotFound();
			_distributionCycleService.Start(DistributionCycle);
			return Ok(new { success = true });
		}

		[HttpPut("upload_distribution_image/{id}")]
		public IActionResult UploadDistributionImage(int id, IFormFile picture)
		{
			var distributionCycle = _distributionCycleService.GetById(id);
			if (distributionCycle == null)
				return NotFound();
			_distributionCycleService.UploadDistributionImage(distributionCycle, picture);
			return Ok(new { success = true });
		}


		[HttpPut("resend_distribution_code/{id}")]
		public IActionResult ResendDistributionCode(int id, [FromBody]ResendDistributionCodeModel model)
		{
			var DistributionCycle = _distributionCycleService.GetDetailsById(id);
			if (DistributionCycle == null)
				return NotFound();
			_distributionCycleService.ResendDistributionCode(DistributionCycle, model);
			return Ok(new { success = true });
		}

		[HttpPut("accept/{id}")]
		public IActionResult Accept(int id, [FromBody] AcceptDistributionCodeModel model)
		{
			var distributionCycle = _distributionCycleService.GetDetailsById(id);
			if (distributionCycle == null)
				return NotFound();

			var dcontract = distributionCycle.DistributionCycleHousingContracts.FirstOrDefault(x => x.DistributionCode == model.DistributionCode && x.HousingContractId == model.HousingContractId);
			if (dcontract == null)
			{
				return BadRequest("code not found");
			}
			_distributionCycleService.Accept(distributionCycle, model);
			return Ok(new { success = true });
		}

		[HttpGet("not_seen_cycles")]
		public IActionResult GetNotSeenCycles()
		{
			var DistributionCycles = _distributionCycleService.GetNotSeenCycles(_user);
			return Ok(DistributionCycles);
		}

		[HttpPut("mark_cycle_as_seen/{id}")]
		public IActionResult MaskCycleAsSeen(int id)
		{
			var distributionCycle = _distributionCycleService.GetById(id);
			if (distributionCycle == null)
				return NotFound();

			_distributionCycleService.MarkCycleSeen(distributionCycle);
			return Ok(distributionCycle);
		}

		[HttpPut("return_quantity/{id}")]
		public IActionResult ReturnedQuantity(int id, [FromBody] ReturnQuantityModel model)
		{
			var distributionCycle = _distributionCycleService.GetDetailsById(id);
			if (distributionCycle == null)
				return NotFound();

			_distributionCycleService.ReturnQuantity(distributionCycle, model);
			return Ok(new { success = true });
		}

		[HttpPut("mark_cycle_as_completed/{id}")]
		public IActionResult MaskCycleAsCompleted(int id)
		{
			var distributionCycle = _distributionCycleService.GetById(id);
			if (distributionCycle == null)
				return NotFound();

			_distributionCycleService.MarkCycleCompleted(distributionCycle);
			return Ok(distributionCycle);
		}

		[AllowAnonymous]
		[HttpGet("forReports")]
		public IActionResult GetForReports([FromQuery] DistributionCycleFilterModel filter)
		{
			var DistributionCycles = _distributionCycleService.GetForReportsWithPagination(filter, _user);
			return Ok(DistributionCycles);
		}

	}
}
