using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Models;
using Zamazimah.Models.Identity;

namespace Zamazimah.Data.Repositories
{
    public class GroupRoleRepository : BaseRepository<GroupRole>, IGroupRoleRepository
    {
        public GroupRoleRepository(ZamazimahContext context) : base(context) { }

        public GroupRole GetByIdGroupRole(int GroupId, int RoleId)
        {
            var query = this.context.GroupRoles.Where(o => o.GroupId == GroupId && o.RoleId == RoleId).FirstOrDefault();
            return query;
        }

        public GroupRoleApiModel GetRoleByGroupId(int GroupId)
        {
            var query = this.context.GroupRoles.Where(o => o.GroupId == GroupId).Select(i => new GroupRoleViewModel
            {
                RoleId = i.RoleId
            }
            );
            var count = query.Count();
            var result = new GroupRoleApiModel
            {
                TotalCount = count
            };
            var items = query.ToList();
            result.Data = items;
            return result;
        }
    }
    public interface IGroupRoleRepository : IRepository<GroupRole>
    {
        GroupRole GetByIdGroupRole(int GroupId, int RoleId);
        GroupRoleApiModel GetRoleByGroupId(int GroupId);
    }
}
