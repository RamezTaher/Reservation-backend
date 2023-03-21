using Zamazimah.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Zamazimah.Data.Repositories;
using Zamazimah.Models.Identity;

namespace Zamazimah.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IApplicationUserRepository _userRepository;
        public GroupService(IGroupRepository groupRepository, IApplicationUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public GroupApiModel Get(string filter, int? ZoneId, int page = 1, int take = 4)
        {
            return _groupRepository.GetGroups(filter, ZoneId, page, take);
        }

        public GroupApiModel GetAllGroups(int? ZoneId)
        {
            return _groupRepository.GetAllGroups(ZoneId);
        }

        public Group GetById(int id)
        {
            return _groupRepository.GetById(id);
        }

        public int Update(Group oldGroup, Group group)
        {
            bool exist = _groupRepository.IsExistGroupWithName(group.Name, oldGroup.ZoneId);
            if (!exist)
            {
                oldGroup.Name = group.Name;
                int rows = _groupRepository.SaveChanges();
                return rows;
            }
            return 0;
        }

        public int Delete(Group group)
        {
            _groupRepository.Remove(group);
            int rows = _groupRepository.SaveChanges();
            return rows;
        }

        public bool Insert(Group group)
        {
            bool exist = _groupRepository.IsExistGroupWithName(group.Name, group.ZoneId);
            if (!exist)
            {
                _groupRepository.Insert(group);
                _groupRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public bool IsExistGroupWithName(string Name, int? ZoneId)
        {
            bool exist = _groupRepository.IsExistGroupWithName(Name, ZoneId);
            return exist;
        }

        public bool IsDefaultGroupWithId(int groupId)
        {
            bool exist = _groupRepository.IsDefaultGroupWithId(groupId);
            return exist;
        }
    }

    public interface IGroupService
    {
        GroupApiModel Get(string filter, int? ZoneId, int page = 1, int take = 4);
        GroupApiModel GetAllGroups(int? ZoneId);
        Group GetById(int id);
        bool Insert(Group group);
        int Update(Group oldGroup, Group group);
        int Delete(Group group);
        bool IsExistGroupWithName(string Name, int? ZoneId);
        bool IsDefaultGroupWithId(int groupId);
    }
}
