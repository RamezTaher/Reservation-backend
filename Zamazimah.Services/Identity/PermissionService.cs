using Zamazimah.Data.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Zamazimah.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public List<PermissionViewModel> GetAllPermissions()
        {
            return _permissionRepository.GetAllPermissions();
        }

        //public List<Permission> GeneratePermissions()
        //{
        //    List<Permission> permissions = new List<Permission>();
        //    foreach (var item in PermissionConstants.guichet)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.guichetEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.operations)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.operationsEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.caisses)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.caissesEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.banks)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.banksEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.user_managment)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.user_managmentEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.cours)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.coursEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.clients)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.clientsEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.suivies)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.suiviesEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.parametres)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.parametresEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.depenses)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.depensesEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.exchangeBetweenOffices)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.exchangeBetweenOfficesEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    foreach (var item in PermissionConstants.dashbaord)
        //    {
        //        permissions.Add(new Permission { Name = item, Entity = PermissionConstants.DashboardEntity, Module = ApplicationConstants.AppModule, ApplicationName = ApplicationConstants.BackendApplicationName });
        //    }
        //    return permissions;
        //}

        //public void SeedPermissions(RoleManager<Permission> roleManager)
        //{
        //    var permissions = GeneratePermissions();
        //    foreach (var item in permissions)
        //    {
        //        var result = roleManager.CreateAsync(item).Result;
        //    }
        //    _permissionRepository.SaveChanges();
        //}
        public List<PermissionEntityModel> GetRolePermissions(int roleId)
        {
            return _permissionRepository.GetRolePermissions(roleId);
        }

    }

    public interface IPermissionService
    {
        List<PermissionViewModel> GetAllPermissions();
        //List<Permission> GeneratePermissions();
        //void SeedPermissions(RoleManager<Permission> roleManager);
        List<PermissionEntityModel> GetRolePermissions(int roleId);
    }
}
