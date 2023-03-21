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
    public class UserRoleRepository : BaseRepository<ApplicationUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ZamazimahContext context) : base(context) { }

        public void RemoveMany(IEnumerable<ApplicationUserRole> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            foreach (var item in entities)
            {
                var local = context.Set<ApplicationUserRole>()
                            .Local
                            .FirstOrDefault(entry => entry.RoleId == item.RoleId && entry.ApplicationUserId == item.ApplicationUserId);

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
    public interface IUserRoleRepository : IRepository<ApplicationUserRole>
    {
        new void RemoveMany(IEnumerable<ApplicationUserRole> entities);
    }
}
