using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Models.Stores;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class StoreController : BaseController
	{

		private IStoreService _storeService;


		public StoreController(IStoreService storeService, IUserService userService) : base(userService)
		{
			_storeService = storeService;
		}

		[HttpGet]
		public IActionResult Get([FromQuery]StoreFilterModel filter)
		{
			var Stores = _storeService.GetWithPagination(filter);
			return Ok(Stores);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var Store = _storeService.GetById(id);
			return Ok(Store);
		}

		[HttpPost]
		public IActionResult Create(CreateStoreModel model)
		{
			return Ok(new { success = true, id = _storeService.Create(model) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreateStoreModel model)
		{
			var Store = _storeService.GetById(id);
			if (Store == null)
				return NotFound();
			_storeService.Update(Store, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var Store = _storeService.GetById(id);
			if (Store == null)
				return NotFound();
			_storeService.Delete(Store);
			return Ok(new { success = true });
		}

		[HttpGet("autocomplete")]
		public ActionResult AutoComplete(string? query)
		{
			var results = _storeService.AutoComplete(query);
			return Ok(results);
		}

	}
}
