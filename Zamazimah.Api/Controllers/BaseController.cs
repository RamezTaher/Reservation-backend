using System.Net;
using System.Security.Claims;
using Zamazimah.Core.Constants;
using Zamazimah.Entities.Identity;
using Zamazimah.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zamazimah.Api.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationUser _user;
        protected IUserService _userService;
        protected string client_ip = null;
        //protected string base_url = null;
        public BaseController(IUserService userService)
        {
            _userService = userService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           // this.base_url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/";
            var username = (this.User.Identity as ClaimsIdentity)?.Name;
            this._user = _userService.GetByUserName(username);
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata.Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
            if (_user == null && !hasAllowAnonymous)
            {
                context.Result = new UnauthorizedObjectResult(HttpStatusCode.Unauthorized);
            }
            this.client_ip = HttpContext.Connection.RemoteIpAddress.ToString();
            base.OnActionExecuting(context);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetAcceptLanguageHeader()
        {
            Request.Headers.TryGetValue("Accept-Language", out var headerValue);
            if(headerValue == "ar")
            {
                return Langs.AR;
            }
            if (headerValue == "en")
            {
                return Langs.EN;
            }
            return headerValue;
        }

    }
}
