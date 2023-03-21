using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Api.ModelBinders;
using Zamazimah.Models.PilgrimsTrips;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class PilgrimsTripController : BaseController
	{

		private IPilgrimsTripService _pilgrimsTripService;


		public PilgrimsTripController(IPilgrimsTripService pilgrimsTripService, IUserService userService) : base(userService)
		{
			_pilgrimsTripService = pilgrimsTripService;
		}

		[HttpGet]
		public IActionResult Get([FromQuery] PilgrimsTripFilterModel filter)
		{
			var PilgrimsTrips = _pilgrimsTripService.GetWithPagination(filter, _user);
			return Ok(PilgrimsTrips);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var PilgrimsTrip = _pilgrimsTripService.GetDetailsById(id);
			return Ok(PilgrimsTrip);
		}

		[HttpPost]
		public IActionResult Create(CreatePilgrimsTripModel model)
		{
			return Ok(new { success = true, id = _pilgrimsTripService.Create(model) });
		}

		[HttpPost("urgent_trip")]
		public IActionResult CreateUrgentTrip([ModelBinder(BinderType = typeof(JsonModelBinder))] CreateUrgentTripPilgrimsTripModel model, IFormFile? picture = null)
		{
			return Ok(new { success = true, id = _pilgrimsTripService.CreateUrgentTrip(model, _user, picture) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreatePilgrimsTripModel model)
		{
			var PilgrimsTrip = _pilgrimsTripService.GetById(id);
			if (PilgrimsTrip == null)
				return NotFound();
			_pilgrimsTripService.Update(PilgrimsTrip, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var PilgrimsTrip = _pilgrimsTripService.GetById(id);
			if (PilgrimsTrip == null)
				return NotFound();
			_pilgrimsTripService.Delete(PilgrimsTrip);
			return Ok(new { success = true });
		}

		[HttpGet("autocomplete")]
		public ActionResult AutoComplete(string? query)
		{
			var results = _pilgrimsTripService.AutoComplete(query);
			return Ok(results);
		}

		[HttpPut("mark_as_done/{id}")]
		public IActionResult MarkAsDone(int id, PilgrimsTripDistributionCompletedModel model)
		{
			var PilgrimsTrip = _pilgrimsTripService.GetById(id);
			if (PilgrimsTrip == null)
				return NotFound();
			_pilgrimsTripService.MarkAsDone(PilgrimsTrip, model, _user);
			return Ok(new { success = true });
		}

		[AllowAnonymous]
		[HttpGet("import_pilgrim_trips_from_zamazimah_db")]
		public ActionResult ImportPilgrimsTripsFromZamazimahDB()
		{
			_pilgrimsTripService.ImportFromZamazimahOracleDB();
			return Ok();
		}

		[HttpGet("distributors_performance")]
		public IActionResult GetDistributorsPerformance()
		{
			List<DistributorPerformanceModel> PilgrimsTrip = _pilgrimsTripService.GetDistributorsPerformance();
			return Ok(PilgrimsTrip);
		}

	}
}
