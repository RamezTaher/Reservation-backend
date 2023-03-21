using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Models.Attachements;
using Zamazimah.Services;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AttachementController : BaseController
	{

		private IAttachementService _attachementService;


		public AttachementController(IAttachementService attachementService, IUserService userService) : base(userService)
		{
			_attachementService = attachementService;
		}

		[HttpGet]
		public IActionResult Get(string entityId, EntityType entityType)
		{
			var Attachements = _attachementService.GetAttachements(entityId, entityType);
			return Ok(Attachements);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var Attachement = _attachementService.GetById(id);
			return Ok(Attachement);
		}

		[HttpPost]
		public IActionResult Create(CreateAttachementModel model)
		{
			return Ok(new { success = true, id = _attachementService.Create(model) });
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, CreateAttachementModel model)
		{
			var Attachement = _attachementService.GetById(id);
			if (Attachement == null)
				return NotFound();
			_attachementService.Update(Attachement, model);
			return Ok(new { success = true });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var Attachement = _attachementService.GetById(id);
			if (Attachement == null)
				return NotFound();
			_attachementService.Delete(Attachement);
			return Ok(new { success = true });
		}

	}
}
