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
    public class ApplicationUserGroupRepository : BaseRepository<ApplicationUserGroup>, IApplicationUserGroupRepository
    {
        public ApplicationUserGroupRepository(ZamazimahContext context) : base(context) { }

        public ApplicationUserGroup GetByIdUserGroup(string UserId, int GroupId)
        {
            var query = this.context.ApplicationUserGroups.Where(o => o.ApplicationUserId == UserId && o.GroupId == GroupId).FirstOrDefault();
            return query;
        }

        public GroupUserApiModel GetGroupByUserId(string ApplicationUserId)
        {
            var query = this.context.ApplicationUserGroups.Where(o => o.ApplicationUserId == ApplicationUserId).Select(i => new GroupUserViewModel
            {
                GroupId = i.GroupId,
                ApplicationUserId = i.ApplicationUserId
            }
            );
            var count = query.Count();
            var result = new GroupUserApiModel
            {
                TotalCount = count
            };
            var items = query.ToList();
            result.Data = items;
            return result;
        }
    }
    public interface IApplicationUserGroupRepository : IRepository<ApplicationUserGroup>
    {
        ApplicationUserGroup GetByIdUserGroup(string UserId, int GroupId);
        GroupUserApiModel GetGroupByUserId(string ApplicationUserId);
    }
}
