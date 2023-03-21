using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Models.Identity;

namespace Zamazimah.Data.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(ZamazimahContext context) : base(context) { }

        public GroupApiModel GetGroups(string GroupName, int? ZoneId, int page = 0, int take = 0)
        {
            var query = this.context.Groups.Where(o => o.ZoneId == ZoneId).Select(i => new GroupViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    UsersCount =i.ApplicationUserGroups.Count(),
                    ZoneId=i.ZoneId,
                    IsDefault = i.IsDefault
            }
            );
            query = query.Where(i => (i.Name.Contains(GroupName) || GroupName == string.Empty || GroupName == null));
            var count = query.Count();
            var result = new GroupApiModel
            {
                TotalCount = count,
            };
            query = query.Skip(Math.Max(0, page - 1) * take);
            if (take != 0)
            {
                query = query.Take(take);
            }
            var items = query.ToList();          
            result.Data = items;
            return result;
        }

        public GroupApiModel GetAllGroups(int? ZoneId)
        {
            var query = this.context.Groups.Where(o => o.ZoneId == ZoneId).Select(i => new GroupViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    IsDefault = i.IsDefault
            }
            );
            var count = query.Count();
            var result = new GroupApiModel
            {
                TotalCount = count,
            };
            var items = query.ToList();
            result.Data = items;
            return result;
        }

        public bool IsExistGroupWithName(string groupName, int? zoneId)
        {
            IsExist(o => (o.Name == groupName && o.ZoneId == zoneId) || string.IsNullOrEmpty(groupName));
            return false;
        }

        public bool IsDefaultGroupWithId(int groupId)
        {
            return IsExist(o => o.IsDefault == true && o.Id == groupId);
        }
    }
    public interface IGroupRepository : IRepository<Group>
    {
        GroupApiModel GetGroups(string GroupName, int? ZoneId, int page = 0, int take = 0);
        GroupApiModel GetAllGroups(int? ZoneId);
        bool IsExistGroupWithName(string groupName, int? zoneId);
        bool IsDefaultGroupWithId(int groupId);
    }
}
