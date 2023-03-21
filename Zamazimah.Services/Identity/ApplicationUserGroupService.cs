using Zamazimah.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Zamazimah.Data.Repositories;
using Zamazimah.Models.Identity;

namespace Zamazimah.Services
{
    public class ApplicationUserGroupService : IApplicationUserGroupService { 

        private readonly IApplicationUserGroupRepository _applicationUserGroupRepository;

        public ApplicationUserGroupService(IApplicationUserGroupRepository applicationUserGroupRepository)
        {
            _applicationUserGroupRepository = applicationUserGroupRepository;
        }

        public int Delete(ApplicationUserGroup applicationUserGroup)
        {
            _applicationUserGroupRepository.Remove(applicationUserGroup);
            int rows = _applicationUserGroupRepository.SaveChanges();
            return rows;
        }

        public int Insert(ApplicationUserGroup applicationUserGroup)
        {
            _applicationUserGroupRepository.Insert(applicationUserGroup);
            _applicationUserGroupRepository.SaveChanges();

            return applicationUserGroup.GroupId;
        }

        public ApplicationUserGroup GetByIdUserGroup(string UserId, int GroupId)
        {
            return _applicationUserGroupRepository.GetByIdUserGroup(UserId, GroupId);
        }

        public GroupUserApiModel GetGroupByUserId(string UserId)
        {
            return _applicationUserGroupRepository.GetGroupByUserId(UserId);
        }
    }

    public interface IApplicationUserGroupService
    {
        int Insert(ApplicationUserGroup applicationUserGroup);
        int Delete(ApplicationUserGroup applicationUserGroup);
        ApplicationUserGroup GetByIdUserGroup(string UserId, int GroupId);
        GroupUserApiModel GetGroupByUserId(string UserId);
    }
}
