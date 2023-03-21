using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Api.ModelBinders;
using Zamazimah.Data.OracleDbConnection;
using Zamazimah.Models.HousingContracts;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class HousingContractController : BaseController
	{

		private IHousingContractService _housingContractService;
		private readonly IConfiguration _configuration;
		private string base_url;

        public HousingContractController(IHousingContractService housingContractService,
			IUserService userService,
			IConfiguration configuration) : base(userService)
		{
			_housingContractService = housingContractService;
			_configuration = configuration;
			this.base_url = _configuration["BASE_URL"];
		}

		[HttpGet]
		public IActionResult Get([FromQuery]HousingContractFilterModel filter)
		{
			var housingContracts = _housingContractService.GetWithPagination(filter);
			return Ok(housingContracts);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var housingContract = _housingContractService.GetDetailsById(id, this.base_url);
			if (housingContract == null)
				return NotFound();
			return Ok(housingContract);
		}

		[HttpPost]
		public IActionResult Create([ModelBinder(BinderType = typeof(JsonModelBinder))] CreateHousingContractModel model, IFormFile? picture = null)
		{
			return Ok(new { success = true, id = _housingContractService.Create(model, picture) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreateHousingContractModel model)
		{
			var housingContract = _housingContractService.GetById(id);
			if (housingContract == null)
				return NotFound();
			_housingContractService.Update(housingContract, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var housingContract = _housingContractService.GetDetailsById(id, "");
			if (housingContract == null)
				return NotFound();
			int result  = _housingContractService.Delete(housingContract);
			if(result == -1)
            {
				return Ok(new { success = false, message = "لا يمكن حذف العقد لأنه مرتبط بدورات توزيع" });
			}
			return Ok(new { success = true });
		}

		[HttpGet("autocomplete")]
		public ActionResult AutoComplete(string? query, DateTime? distributionDate = null)
		{
			var results = _housingContractService.AutoComplete(_user, query, distributionDate);
			return Ok(results);
		}

		[HttpPost("import_from_excel")]
		public ActionResult ImportFromExcel(IFormFile file)
		{
			var result = _housingContractService.ImportFromExcel(file);
			return Ok(result);
		}

		//[AllowAnonymous]
		//[HttpGet("import_house_contracts_from_zamazimah_db")]
		//public ActionResult ImportFromZamazimahOracleDB()
		//{
		//	var rows = _housingContractService.ImportFromZamazimahOracleDB();
		//	return Ok(rows);
		//}

	}
}
