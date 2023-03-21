using Zamazimah.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Zamazimah.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SettingsController : BaseController
    {
        private readonly ISettingsService _settingsService;
        public SettingsController(IUserService userService, ISettingsService settingsService) : base(userService)
        {
            _settingsService = settingsService;
        }

        [HttpGet("countries")]
        public IActionResult GetAllCountries()
        {
            var result = _settingsService.GetAllCountries(this.GetAcceptLanguageHeader());
            return Ok(result);
        }

        [HttpGet("centers")]
        public IActionResult GetAllCenters()
        {
            var result = _settingsService.GetAllCenters(this.GetAcceptLanguageHeader());
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("cities")]
        public IActionResult GetAllCities()
        {
            var result = _settingsService.GetAllCities(this.GetAcceptLanguageHeader());
            return Ok(result);
        }

        [HttpGet("locationNatures")]
        public IActionResult GetAllLocationNatures()
        {
            var result = _settingsService.GetAllLocationNatures(this.GetAcceptLanguageHeader());
            return Ok(result);
        }

        [HttpGet("distributionPoints")]
        public IActionResult GetAllDistributionPoints()
        {
            var result = _settingsService.GetAllDistributionPoints(this.GetAcceptLanguageHeader());
            return Ok(result);
        }

        [HttpGet("transport_companies")]
        public IActionResult GetAllTransportCompanies(string? query = null)
        {
            var result = _settingsService.GetTransportCompaniesAutocomplete(query, this.GetAcceptLanguageHeader());
            return Ok(result);
        }

    }
}
