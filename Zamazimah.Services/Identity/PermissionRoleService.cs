using Zamazimah.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Zamazimah.Data.Repositories;
using Zamazimah.Models.Identity;
using System.Linq;

namespace Zamazimah.Services
{
    public class PermissionRoleService : IPermissionRoleService
    {

        private readonly IPermissionRoleRepository _permissionRoleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public PermissionRoleService(IPermissionRoleRepository permissionRoleRepository, IPermissionRepository permissionRepository)
        {
            _permissionRoleRepository = permissionRoleRepository;
            _permissionRepository = permissionRepository;
        }

        public int Delete(PermissionRole permissionRole)
        {
            _permissionRoleRepository.Remove(permissionRole);
            int rows = _permissionRoleRepository.SaveChanges();
            return rows;
        }

        public int DeleteMany(List<PermissionRole> permissionRoles)
        {
            foreach (PermissionRole permissionRole in permissionRoles)
            {
                _permissionRoleRepository.Remove(permissionRole);
            }
            int rows = _permissionRoleRepository.SaveChanges();

            return rows;
        }

        public int Insert(PermissionRole permissionRole)
        {
            _permissionRoleRepository.Insert(permissionRole);
            _permissionRoleRepository.SaveChanges();

            return permissionRole.RoleId;
        }

        public int ToggleManyPermissions(int roleId, List<string> permissions)
        {
            var permissionRoles = _permissionRepository.GetCheckedRolePermissions(roleId, permissions);
            var rolePermissionsToInsert = new List<PermissionRole>();
            var rolePermissionsToDelete = new List<PermissionRole>();
            foreach (string permission in permissions)
            {
                if (permissionRoles.Any(x=>x.PermissionId == permission && !x.IsChecked)) {
                    rolePermissionsToInsert.Add(new PermissionRole { PermissionId = permission, RoleId = roleId });
                }
                if (permissionRoles.Any(x => x.PermissionId == permission && x.IsChecked))
                {
                    rolePermissionsToDelete.Add(new PermissionRole { PermissionId = permission, RoleId = roleId });
                }
            }
            _permissionRoleRepository.InsertMultiple(rolePermissionsToInsert);
            _permissionRoleRepository.RemoveMany(rolePermissionsToDelete);
            int rows = _permissionRoleRepository.SaveChanges();
            return rows;
        }

        public PermissionRole GetByIdPermissionRole(int RoleId, string PermissionId)
        {
            return _permissionRoleRepository.GetByIdPermissionRole(RoleId, PermissionId);
        }

        public List<PermissionRole> GetByIdManyPermissionRole(int RoleId)
        {
            return _permissionRoleRepository.GetByIdManyPermissionRole(RoleId);
        }

        public PermissionRoleApiModel GetPermissionByRoleId(int RoleId)
        {
            return _permissionRoleRepository.GetPermissionByRoleId(RoleId);
        }
    }

    public interface IPermissionRoleService
    {
        int Insert(PermissionRole permissionRole);
        int Delete(PermissionRole permissionRole);
        int DeleteMany(List<PermissionRole> permissionRoles);
        PermissionRole GetByIdPermissionRole(int RoleId, string PermissionId);
        List<PermissionRole> GetByIdManyPermissionRole(int RoleId);
        PermissionRoleApiModel GetPermissionByRoleId(int RoleId);
        int ToggleManyPermissions(int roleId, List<string> permissions);
    }
}
