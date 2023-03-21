using Zamazimah.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Zamazimah.Data.Repositories;
using Zamazimah.Models.Identity;

namespace Zamazimah.Services
{
    public class GroupRoleService : IGroupRoleService
    {

        private readonly IGroupRoleRepository _groupRoleRepository;

        public GroupRoleService(IGroupRoleRepository groupRoleRepository)
        {
            _groupRoleRepository = groupRoleRepository;
        }

        public int Delete(GroupRole groupRole)
        {
            _groupRoleRepository.Remove(groupRole);
            int rows = _groupRoleRepository.SaveChanges();
            return rows;
        }

        public int Insert(GroupRole groupRole)
        {
            _groupRoleRepository.Insert(groupRole);
            _groupRoleRepository.SaveChanges();

            return groupRole.GroupId;
        }

        public GroupRole GetByIdGroupRole(int GroupId, int RoleId)
        {
            return _groupRoleRepository.GetByIdGroupRole(GroupId, RoleId);
        }

        public GroupRoleApiModel GetRoleByGroupId(int RoleId)
        {
            return _groupRoleRepository.GetRoleByGroupId(RoleId);
        }
    }

    public interface IGroupRoleService
    {
        int Insert(GroupRole groupRole);
        int Delete(GroupRole groupRole);
        GroupRole GetByIdGroupRole(int GroupId, int RoleId);
        GroupRoleApiModel GetRoleByGroupId(int RoleId);
    }
}

