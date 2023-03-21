using Zamazimah.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Zamazimah.Data.Repositories;
using Zamazimah.Models.Identity;
using Zamazimah.Generic.Models;

namespace Zamazimah.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public ResultApiModel<IEnumerable<RoleViewModel>> Get(string filter, int? officeId, int page = 1, int take = 4)
        {
            return _roleRepository.GetRoles(filter, officeId, page, take);
        }

        public List<RoleViewModel> GetAllRoles(int? officeId)
        {
            return _roleRepository.GetAllRoles(officeId);
        }

        public Role GetById(int id)
        {
            return _roleRepository.GetById(id);
        }

        public int Update(Role oldRole, Role role)
        {
            oldRole.Name = role.Name;
            int rows = _roleRepository.SaveChanges();
            return rows;
        }

        public int Delete(Role role)
        {
            _roleRepository.Remove(role);
            int rows = _roleRepository.SaveChanges();
            return rows;
        }

        public bool Insert(Role role, ApplicationUser user)
        {
            bool exist = _roleRepository.IsExistRoleWithName(role.Name, 1);
            if (!exist)
            {
                role.IsDefault = false;
                _roleRepository.Insert(role);
                _roleRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public bool IsExistRoleWithName(string roleName, int? ZoneId)
        {
            bool exist = _roleRepository.IsExistRoleWithName(roleName, ZoneId);
            return exist;
        }

        public bool IsDefaultRoleWithId(int groupId)
        {
            bool exist = _roleRepository.IsDefaultRoleWithId(groupId);
            return exist;
        }
    }

    public interface IRoleService
    {
        ResultApiModel<IEnumerable<RoleViewModel>> Get(string filter, int? officeId, int page = 1, int take = 4);
        List<RoleViewModel> GetAllRoles(int? ZoneId);
        Role GetById(int id);
        bool Insert(Role role, ApplicationUser user);
        int Update(Role oldRole, Role role);
        int Delete(Role role);
        bool IsExistRoleWithName(string Name, int? ZoneId);
        bool IsDefaultRoleWithId(int groupId);
    }
}
