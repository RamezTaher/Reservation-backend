using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Models.Vehicles;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class VehicleController : BaseController
	{

		private IVehicleService _vehicleService;


		public VehicleController(IVehicleService vehicleService, IUserService userService) : base(userService)
		{
			_vehicleService = vehicleService;
		}

		[HttpGet]
		public IActionResult Get([FromQuery] VehicleFilterModel filter)
		{
			var Vehicles = _vehicleService.GetWithPagination(filter);
			return Ok(Vehicles);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var Vehicle = _vehicleService.GetById(id);
			return Ok(Vehicle);
		}

		[HttpPost]
		public IActionResult Create(CreateVehicleModel model)
		{
			return Ok(new { success = true, id = _vehicleService.Create(model) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreateVehicleModel model)
		{
			var Vehicle = _vehicleService.GetById(id);
			if (Vehicle == null)
				return NotFound();
			_vehicleService.Update(Vehicle, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var Vehicle = _vehicleService.GetById(id);
			if (Vehicle == null)
				return NotFound();
			_vehicleService.Delete(Vehicle);
			return Ok(new { success = true });
		}

		[HttpGet("autocomplete")]
		public ActionResult AutoComplete(string? query)
		{
			var results = _vehicleService.AutoComplete(query);
			return Ok(results);
		}

	}
}
