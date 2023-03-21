using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zamazimah.Entities.Identity;
using Zamazimah.Models.Identity;
using Zamazimah.Services;
using Zamazimah.Generic.Models;
using System;

namespace Zamazimah.Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IRoleService roleService;
        private readonly IPermissionRoleService permissionRoleService;
        private readonly IPermissionService _permissionService;

        public RoleController(IRoleService roleService, IPermissionRoleService permissionRoleService,
            IPermissionService permissionService, IUserService userService) : base(userService)
        {
            this.roleService = roleService;
            this.permissionRoleService = permissionRoleService;
            this._permissionService = permissionService;
        }



        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            var result = roleService.GetById(id);
            return Ok(result);
        }

        //[HttpGet("all_permissions")]
        //public IActionResult GetAllPermissions()
        //{
        //    var result = _permissionService.GetAllPermissions();
        //    return Ok(result);
        //}

        [HttpGet("role_permissions/{id}")]
        public IActionResult GetPermissionByRoleId(int id)
        {
            var result = _permissionService.GetRolePermissions(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Role role)
        {
            bool roleInserted = roleService.Insert(role, _user);
            if (!roleInserted)
            {
                return Ok("Name role Exist");
            }
            return Ok("");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Role role)
        {
            var oldrole = roleService.GetById(role.Id);
            if (oldrole == null)
            {
                return NotFound();
            }
            roleService.Update(oldrole, role);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var role = roleService.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            if (role.IsDefault == true)
            {
                return Ok("No Delete Admin Roles");
            }
            var exist = roleService.Delete(role);
            return Ok(exist);
        }


        //[HttpDelete("remove_permissions")]
        //public IActionResult DeleteManyPermission(int[] roleId)
        //{
        //    var exist = permissionRoleService.DeleteMany(permissionRole);
        //    return Ok(exist);
        //}

        //[HttpPost("PostPermission")]
        //public IActionResult PostPermission([FromBody] PermissionRole permissionRole)
        //{
        //    var permissionRoleId = permissionRoleService.Insert(permissionRole);
        //    if (permissionRoleId == 0)
        //    {
        //        return Ok("Name role Exist");
        //    }
        //    return Ok(permissionRoleId);
        //}

        [HttpPost("toggle_permissions_to_role/{id}")]
        public IActionResult PostManyPermission(int id,  [FromBody]List<String> permissions)
        {
            permissionRoleService.ToggleManyPermissions(id, permissions);
            return Ok();
        }

    }
}