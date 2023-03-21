using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Entities.Identity;
using Zamazimah.Models.Identity;
using Zamazimah.Services;

namespace Zamazimah.Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IGroupRoleService groupRoleService;
        private readonly IRoleService roleService;
        private readonly IUserService userService;

        public GroupController(IGroupService groupService, IGroupRoleService groupRoleService, IRoleService roleService, IUserService userService)
        {
            this.groupService = groupService;
            this.groupRoleService = groupRoleService;
            this.roleService = roleService;
            this.userService = userService;
        }


 


        [HttpGet("GetRoleByGroupId")]
        public GroupRoleApiModel GetRoleByGroupId([FromQuery] int GroupId)
        {
            var result = new GroupRoleApiModel();
            result = groupRoleService.GetRoleByGroupId(GroupId);
            result.Success = true;
            return result;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Group group)
        {
            bool roleInserted = groupService.Insert(group);
            if (!roleInserted)
            {
                return Ok("Name group Exist");
            }
            return Ok("");
        }

        [HttpPut]
        public ActionResult Put([FromBody]Group group)
        {
            var oldgroup = groupService.GetById(group.Id);
            if (oldgroup == null)
            {
                return NotFound();
            }
            if (oldgroup.IsDefault)
            {
                var username = (this.User.Identity as ClaimsIdentity)?.Name;
                var IsDefaultUser = userService.IsDefaultUserWithUserName(username);
                if (!IsDefaultUser)
                {
                    return Ok("No Edit Admin Group");
                }
            }
            var exist = groupService.Update(oldgroup, group);
            if (exist == 0)
            {
                return Ok("Name group Exist");
            }
            return Ok("success");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var group = groupService.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            if (group.IsDefault)
            {
                return Ok("No Delete Admin Group");
            }
            var exist = groupService.Delete(group);
            return Ok(exist);
        }

        [HttpDelete("DeleteRole")]
        public ActionResult DeleteRole([FromQuery] int groupId, int roleId)
        {
            var isDefaultGroup = groupService.IsDefaultGroupWithId(groupId);
            if (isDefaultGroup)
            {
                var isDefaultRole = roleService.IsDefaultRoleWithId(roleId);
                if (isDefaultRole)
                {
                    return Ok("No Delete Admin Roles");
                }
            }
            var groupRole = groupRoleService.GetByIdGroupRole(groupId, roleId);
            if (groupRole == null)
            {
                return NotFound();
            }
            var exist = groupRoleService.Delete(groupRole);
            return Ok(exist);
        }

        [HttpPost("PostRole")]
        public ActionResult PostRole([FromBody]GroupRole groupRole)
        {
            var groupRoleId = groupRoleService.Insert(groupRole);
            if (groupRoleId == 0)
            {
                return Ok("Name group Exist");
            }
            return Ok(groupRoleId);
        } 

    }
}