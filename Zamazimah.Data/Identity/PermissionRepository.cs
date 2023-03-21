using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Models;
using Zamazimah.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Zamazimah.Data.Repositories
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ZamazimahContext context) : base(context) { }

        public List<PermissionViewModel> GetAllPermissions()
        {
            var query = this.context.Permissions.Select(i => new PermissionViewModel
            {
                Id = i.Id,
                ApplicationName = i.ApplicationName,
                Name = i.Name,
                Module = i.Module,
                Entity = i.Entity
            });
            var result = query.ToList();
            return result;
        }

        public List<PermissionEntityModel> GetRolePermissions(int roleId)
        {
            var query = dbSet.Include(x=>x.PermissionRoles).Select(i => new PermissionRoleModel
            {
                PermissionId = i.Id,
                PermissionName = i.Name,
                PermissionEntity = i.Entity,
                IsChecked = this.context.PermissionRoles.Any(x=>x.PermissionId == i.Id && x.RoleId == roleId),
            });
            var list = query.ToList();

            var result = list.GroupBy(x => x.PermissionEntity).Select(x => new PermissionEntityModel
            {
                PermissionEntity = x.Key,
                IsChecked = x.All(a=>a.IsChecked),
                Permissions = x.ToList(),
            }).ToList();
            return result;
        }


        public List<PermissioCheckedModel> GetCheckedRolePermissions(int roleId, List<string> permissions)
        {
            var query = dbSet.Include(x => x.PermissionRoles).Where(x => permissions.Contains(x.Id)).Select(x => new PermissioCheckedModel
            {
                PermissionId = x.Id,
                IsChecked = this.context.PermissionRoles.Any(i => i.PermissionId == x.Id && i.RoleId == roleId),
            });
            return query.ToList();
        }

    }
    public interface IPermissionRepository : IRepository<Permission>
    {
        List<PermissionViewModel> GetAllPermissions();
        List<PermissionEntityModel> GetRolePermissions(int roleId);
        List<PermissioCheckedModel> GetCheckedRolePermissions(int roleId, List<string> permissions);
    }
}
