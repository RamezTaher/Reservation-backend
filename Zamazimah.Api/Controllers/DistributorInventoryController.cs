using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Models.DistributorInventory;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DistributorInventoryController : BaseController
    {

        private IDistributorInventoryService _DistributorInventoryService;


        public DistributorInventoryController(IDistributorInventoryService DistributorInventoryService, IUserService userService) : base(userService)
        {
            _DistributorInventoryService = DistributorInventoryService;
        }

        [HttpGet]
        public IActionResult Get(string userId)
        {
            var distributorInventorys = _DistributorInventoryService.GetAll(userId);
            return Ok(new
            {
                inventories = distributorInventorys,
                totalAdded = distributorInventorys.Sum(s => s.AddedQuantity),
                totalConsumed = distributorInventorys.Sum(s => s.ConsumedQuantity),
            });
        }

        [HttpGet("my_inventory")]
        public IActionResult GetMyInventory()
        {
            var distributorInventorys = _DistributorInventoryService.GetAll(_user.Id);
            return Ok(new
            {
                inventories = distributorInventorys,
                totalAdded = distributorInventorys.Sum(s => s.AddedQuantity),
                totalConsumed = distributorInventorys.Sum(s => s.ConsumedQuantity),
                quantity = distributorInventorys.Sum(s => s.AddedQuantity) - distributorInventorys.Sum(s => s.ConsumedQuantity),
            });
        }

        [HttpPost]
        public IActionResult AddQuantity(AddDistributorInventoryModel model)
        {
            return Ok(new { success = true, id = _DistributorInventoryService.AddQuantityToInventory(model) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var DistributorInventory = _DistributorInventoryService.GetById(id);
            if (DistributorInventory == null)
                return NotFound();
            _DistributorInventoryService.Delete(DistributorInventory);
            return Ok(new { success = true });
        }

        [HttpPut("emptify_distributor_inventory/{id}")]
        public IActionResult EmptifyDistributorInventory(string id)
        {
            _DistributorInventoryService.EmptifyDistributorInventory(id);
            return Ok(new { success = true });
        }

    }
}
