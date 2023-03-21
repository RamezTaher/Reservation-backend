using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Generic.Models;
using Zamazimah.Models;
using Zamazimah.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Zamazimah.Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ZamazimahContext context) : base(context) { }
        public ResultApiModel<IEnumerable<RoleViewModel>> GetRoles(string roleName, int? officeId, int page = 0, int take = 0)
        {
            var query = this.context.ApplicationRoles.Select(i => new RoleViewModel
            {
                Id = i.Id,
                Name = i.Name,
                IsDefault = i.IsDefault,
            } );
            query = query.Where(i => (i.Name.Contains(roleName) || roleName == string.Empty || roleName == null));
            return this.Paginate(query, page, take);
        }

        public List<RoleViewModel> GetAllRoles(int? officeId)
        {
            var query = this.context.ApplicationRoles.Select(i => new RoleViewModel
            {
                Id = i.Id,
                Name = i.Name,
                IsDefault = i.IsDefault,
            });
            return query.ToList();
        }

        public bool IsExistRoleWithName(string roleName, int? officeId)
        {
            return IsExist(o => (o.Name == roleName) || string.IsNullOrEmpty(roleName));
        }

        public bool IsDefaultRoleWithId(int roleId)
        {
            return IsExist(o => o.IsDefault == true && o.Id == roleId);
        }

        public List<UserRoleModel> GetUserRoles(int officeId, string userId)
        {
            var query = dbSet.Include(x=>x.ApplicationUserRoles).Select(x => new UserRoleModel
            {
                RoleId = x.Id,
                RoleName = x.Name,
                IsChecked = x.ApplicationUserRoles.Any(a => a.ApplicationUserId == userId && a.RoleId == x.Id),
            });
            return query.ToList();
        }

        public List<UserRoleModel> GetCheckedUserRoles(int officeId, string userId)
        {
            var query = dbSet.Include(x => x.ApplicationUserRoles).Select(x => new UserRoleModel
            {
                RoleId = x.Id,
                IsChecked = this.context.ApplicationUserRoles.Any(i => i.ApplicationUserId == userId && i.RoleId == x.Id),
            });
            return query.ToList();
        }

    }
    public interface IRoleRepository : IRepository<Role>
    {
        ResultApiModel<IEnumerable<RoleViewModel>> GetRoles(string roleName, int? officeId, int page = 0, int take = 0);
        List<RoleViewModel> GetAllRoles(int? officeId);
        bool IsExistRoleWithName(string roleName, int? zoneId);
        bool IsDefaultRoleWithId(int roleId);
        List<UserRoleModel> GetUserRoles(int officeId, string userId);
        List<UserRoleModel> GetCheckedUserRoles(int officeId, string userId);
    }
}
