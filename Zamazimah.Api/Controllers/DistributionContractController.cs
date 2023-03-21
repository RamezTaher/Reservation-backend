using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Models.DistributionContracts;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DistributionContractController : BaseController
	{

		private IDistributionContractService _distributionContractService;

		public DistributionContractController(IDistributionContractService distributionContractService,
			IUserService userService) : base(userService)
		{
			_distributionContractService = distributionContractService;
		}

		[HttpGet]
		public IActionResult Get([FromQuery]DistributionContractFilterModel filter)
		{
			var DistributionContracts = _distributionContractService.GetWithPagination(filter);
			return Ok(DistributionContracts);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var DistributionContract = _distributionContractService.GetById(id);
			return Ok(DistributionContract);
		}

		[HttpPost]
		public IActionResult Create(CreateDistributionContractModel model)
		{
			return Ok(new { success = true, id = _distributionContractService.Create(model) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreateDistributionContractModel model)
		{
			var DistributionContract = _distributionContractService.GetById(id);
			if (DistributionContract == null)
				return NotFound();
			_distributionContractService.Update(DistributionContract, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var DistributionContract = _distributionContractService.GetById(id);
			if (DistributionContract == null)
				return NotFound();
			_distributionContractService.Delete(DistributionContract);
			return Ok(new { success = true });
		}

		[HttpGet("autocomplete")]
		public ActionResult AutoComplete(string? query)
		{
			var results = _distributionContractService.AutoComplete(query);
			return Ok(results);
		}

	}
}
