using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Entities.Identity;
using Zamazimah.Models.Identity;
using Zamazimah.Services;
using Zamazimah.Models;
using Microsoft.AspNetCore.Authorization;
using Zamazimah.Core.Constants;
using System.Linq;
using Zamazimah.Core.Utils;
using Zamazimah.Entities;
using Microsoft.Extensions.Configuration;
using Zamazimah.Core.Utils.Mailing;
using Microsoft.Extensions.Options;
using static Zamazimah.Core.Enums.EntitiesEnums;
using Zamazimah.Data.Repositories;

namespace Zamazimah.Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IPermissionService _permissionsService;
        private readonly IApplicationUserGroupService applicationUserGroupService;
        private readonly IGroupService groupService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IOptions<MailSettings> appSettings;

        public UserController(IUserService userService, IApplicationUserGroupService applicationUserGroupService,
        IGroupService groupService, UserManager<ApplicationUser> userManager,
        IPermissionService permissionsService,
        IConfiguration configuration,
        IOptions<MailSettings> app) : base(userService)
        {
            this.applicationUserGroupService = applicationUserGroupService;
            this.groupService = groupService;
            _userManager = userManager;
            _permissionsService = permissionsService;
            _configuration = configuration;
            appSettings = app;
        }



        [HttpGet("GetGroupByUserId")]
        public GroupUserApiModel GetGroupByUserId([FromQuery] string UserId)
        {
            var result = new GroupUserApiModel();
            result = applicationUserGroupService.GetGroupByUserId(UserId);
            result.Success = true;
            return result;
        }

        [AllowAnonymous]
        [HttpGet("get")]
        public ActionResult Get(string id)
        {
            var user = new GetUserModel();
            user = _userService.GetUserModelById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        [HttpPut("ChangePassword")]
        public async Task<object> ChangePassword([FromBody] UserViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new ApplicationException("Password_Confirmation");
            }
            ApplicationUser user = _userService.GetById(model.Id);
            var newPassword = model.Password;
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, newPassword);
            if (result.Succeeded)
            {
                return true;
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPut("EditUserPassword")]
        public async Task<ActionResult> EditUserPassword([FromBody] EditUserPasswordViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new ApplicationException("Password_Confirmation");
            }
            ApplicationUser user = _userService.GetById(model.UserId);
            var newPassword = model.Password;
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, newPassword);
            if (result.Succeeded)
            {
                return Ok(new { message = "Password Updated succuesfully" });
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPost("SendPasswordToMail/{id}")]
        public async Task<ActionResult> SendPaswordToMail(string id)
        {
            ApplicationUser user = _userService.GetById(id);
            if (user.IsDefault == true)
            {
                var username = (this.User.Identity as ClaimsIdentity)?.Name;
                var IsDefaultUser = _userService.IsDefaultUserWithUserName(username);
                if (!IsDefaultUser)
                {
                    return BadRequest(new { message = "No Edit Admin Password" });
                }
            }
            var newPassword = PasswordGeneratorHelper.GeneratePassword(true, true, true, true, 15);

            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, newPassword);
            if (result.Succeeded)
            {
                // MailingHelper.SendEmail("Récupération de mot de passe",user.Email,EmailRecoveryTemplate.EmailTemplate(user.FullName,newPassword),
                //    _configuration);
                return Ok(new { message = "Password Sent succuesfully" });
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpDelete("DeleteGroup")]
        public ActionResult DeleteGroup([FromQuery] string UserId, int GroupId)
        {
            var isDefaultUser = _userService.IsDefaultUserWithId(UserId);
            if (isDefaultUser)
            {
                var isDefaultGroup = groupService.IsDefaultGroupWithId(GroupId);
                if (isDefaultGroup)
                {
                    return Ok("No Delete Admin Groups");
                }
            }
            var userGroup = applicationUserGroupService.GetByIdUserGroup(UserId, GroupId);
            if (userGroup == null)
            {
                return NotFound();
            }
            var exist = applicationUserGroupService.Delete(userGroup);
            return Ok(exist);
        }

        [HttpPost("PostGroup")]
        public ActionResult PostGroup([FromBody] ApplicationUserGroup userGroup)
        {
            var userGroupId = applicationUserGroupService.Insert(userGroup);
            if (userGroupId == 0)
            {
                return Ok("Name group Exist");
            }
            return Ok(userGroupId);
        }

        [HttpGet("GetByUserNamePermissions")]
        public List<string> GetByUserNamePermissions(string username)
        {
            var result = new List<string>();
            result = _userService.GetUserPermissions(username);
            return result;
        }

        [HttpGet("GetUserPermissions")]
        public List<string> GetUserPermissions()
        {
            var result = new List<string>();
            result = _userService.GetUserPermissions(_user.UserName);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("users")]
        public ActionResult GetUsers([FromQuery]UserFilterModel filter)
        {
            var users = _userService.GetUsers(filter);
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("distributors")]
        public ActionResult GetDistributors([FromQuery] DistributorFilterModel filter)
        {
            var users = _userService.GetDistributors(filter);
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("drivers")]
        public ActionResult GetDrivers([FromQuery] DriverFilterModel filter)
        {
            var users = _userService.GetDrivers(filter);
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Post([FromBody] CreateUserViewModel model)
        {
            var messages = new List<string>();
            //if (_userService.IsExist(x => x.UserName == model.Email))
            //{
            //    messages.Add(ValidationsErrorCodes.UserNameExistMessage);
            //}
            if (_userService.IsExist(x => x.Email == model.Email))
            {
                messages.Add(ValidationsErrorCodes.EmailExistMessage);
            }
            if (_userService.IsExist(x => x.Phone == model.Phone))
            {
                messages.Add(ValidationsErrorCodes.PhoneExistMessage);
            }
            if (messages.Any())
            {
                return BadRequest(new { success = false, messages = messages });
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Phone2 = model.Phone2,
                Email = model.Email,
                Position = model.Position,
                UserName = model.IdentityNumber,
                CountryId = model.CountryId,
                Lang = model.Lang,
                IdentityNumber = model.IdentityNumber,
                IdentityExpiration = model.IdentityExpiration,
                BirthDate = model.BirthDate,
                NominationDate = model.NominationDate,
                UserType = model.UserType,
                DrivingLicense = model.DrivingLicense,
                DrivingLicenseExpiration = model.UserType == PredefinedUserType.DRIVER ? model.DrivingLicenseExpiration : DateTime.Now,
                DistributionPointId = model.DistributionPointId,
                Code = model.Code,
                // representative 1
                ViceFirstName = model.ViceFirstName,
                ViceLastName = model.ViceLastName,
                ViceEmail = model.ViceEmail,
                VicePhone = model.VicePhone,
                // representative 2
                ViceFirstName2 = model.ViceFirstName2,
                ViceLastName2 = model.ViceLastName2,
                ViceEmail2 = model.ViceEmail2,
                VicePhone2 = model.VicePhone2,
                // representative 3
                ViceFirstName3 = model.ViceFirstName3,
                ViceLastName3 = model.ViceLastName3,
                ViceEmail3 = model.ViceEmail3,
                VicePhone3 = model.VicePhone3,
                CenterId = model.CenterId,
            };
            string password = PasswordGeneratorHelper.GeneratePassword(true, true, true, true, 10);
            if(model.UserType == PredefinedUserType.DISTRIBUTOR)
            {
                password = "20222022";
            }
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                MailSettings mailSettings = appSettings.Value;
                mailSettings.EnableSsl = false;
                MessageModel message = new MessageModel
                {
                    Subject = "مشروع الزمازمة - بيانات الحساب",
                    Body = "السلام عليكم ورحمة الله وبركاته " + "<br/>" +
                    "تجدون هنا بيانات حسابكم" + "<br/>" +
                    "اسم المستخدم : " + user.IdentityNumber + "<br/>" +
                    "كلمة المرور : " + password,
                    To = new List<string> { model.Email },
                    From = mailSettings.Sender,
                };

                SMTPMailSender.SendMail(message, mailSettings);
                return true;
            }
            return false;
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.IsDefault == true)
            {
                return Ok("No Delete Admin User");
            }
            var exist = _userService.Delete(user);
            if(exist == -1)
            {
                return Ok(new { success = false, message = "لا يمكن حذف هذا المستخدم لأنه مرتبط بعقود سكن أو بدورات توزيع" });
            }
            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] CreateUserViewModel model)
        {
            var oldUser = _userService.GetById(id);
            if (oldUser == null)
            {
                return NotFound();
            }
            var exist = _userService.Update(oldUser, model);
            return Ok(new { success = true });
        }

        [HttpGet("user_types")]
        public ActionResult GetPredefinedUserTypes()
        {
            var results = _userService.GetPredefinedUserTypes();
            return Ok(results);
        }
        [AllowAnonymous]

        [HttpGet("autocomplete")]
        public ActionResult AutoComplete(string? query, PredefinedUserType userType = 0, int entryPointId = 0)
        {
            var results = _userService.AutoComplete(_user, query, userType, entryPointId);
            return Ok(results);
        }

        [HttpPost("import_distributors_from_excel")]
        public ActionResult ImportDistributorsFromExcel(IFormFile file)
        {
            var result = _userService.ImportDistributorsFromExcel(file);
            return Ok(result);
        }
    }
}
