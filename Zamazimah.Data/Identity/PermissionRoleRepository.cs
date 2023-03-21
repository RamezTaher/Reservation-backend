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
    public class PermissionRoleRepository : BaseRepository<PermissionRole>, IPermissionRoleRepository
    {
        public PermissionRoleRepository(ZamazimahContext context) : base(context) { }

        public PermissionRole GetByIdPermissionRole(int RoleId, string PermissionId)
        {
            var query = this.context.PermissionRoles.Where(o => o.RoleId == RoleId && o.PermissionId == PermissionId).FirstOrDefault();
            return query;
        }

        public List<PermissionRole> GetByIdManyPermissionRole(int RoleId)
        {
            var query = this.context.PermissionRoles.Where(o => o.RoleId == RoleId);
            return query.ToList();
        }

        public PermissionRoleApiModel GetPermissionByRoleId(int RoleId)
        {
            var query = this.context.PermissionRoles.Where(o => o.RoleId == RoleId).Select(i => new PermissionRoleViewModel
            {
                    PermissionId = i.PermissionId              
                }
            );
            var count = query.Count();
            var result = new PermissionRoleApiModel
            {
                TotalCount = count
            };
            var items = query.ToList();
            result.Data = items;
            return result;
        }

        public new void RemoveMany(IEnumerable<PermissionRole> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            foreach (var item in entities)
            {
                var local = context.Set<PermissionRole>()
                            .Local
                            .FirstOrDefault(entry => entry.RoleId == item.RoleId && entry.PermissionId == item.PermissionId);

                // check if local is not null 
                if (local != null)
                {
                    // detach
                    context.Entry(local).State = EntityState.Detached;
                }
            }
            dbSet.RemoveRange(entities);
        }

    }
    public interface IPermissionRoleRepository : IRepository<PermissionRole>
    {
        PermissionRole GetByIdPermissionRole(int RoleId, string PermissionId);
        List<PermissionRole> GetByIdManyPermissionRole(int RoleId);
        PermissionRoleApiModel GetPermissionByRoleId(int RoleId);
        new void RemoveMany(IEnumerable<PermissionRole> entities);
    }
}
