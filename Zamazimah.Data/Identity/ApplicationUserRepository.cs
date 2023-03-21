using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Models.Identity;
using Zamazimah.Models;
using Zamazimah.Generic.Models;
using static Zamazimah.Core.Enums.EntitiesEnums;
using Zamazimah.Models.DistributionCycles;

namespace Zamazimah.Data.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ZamazimahContext context) : base(context) { }


        public ApplicationUser GetByUserName(string userName)
        {
            return context.Users.Include(x => x.ApplicationUserRoles).ThenInclude(x => x.Role).ThenInclude(x => x.PermissionRoles).ThenInclude(x => x.Permission)
                .Where(u => u.UserName == userName &&
                (u.UserType != PredefinedUserType.DRIVER && u.UserType != PredefinedUserType.REPRESENTATIVE)
                ).FirstOrDefault();
        }

        public DistributorStats GetDistributorStatistics(string userId)
        {
            return new DistributorStats
            {
                NumberOfCompletedCycles = context.DistributionCycles.Where(x => x.DistributorId == userId && x.Status == DistributionCycleStatus.Completed).Count(),
                CumulativeBottles = context.PilgrimsTrips.Where(x => x.DistributorId == userId).Sum(x => x.DistributedQuantity - (x.PilgrimsNumber * 2)),
            };
        }


        public ApplicationUser GetByEmail(string Email)
        {
            return context.Users.Where(u => u.Email == Email).FirstOrDefault();
        }

        public bool IsExistUserName(string userName, int? zoneId)
        {
            return context.Users.Any(o => (o.UserName == userName)) || string.IsNullOrEmpty(userName);
        }

        public List<string> GetUserPermissions(string username)
        {
            var result = context.Users.Where(u => u.UserName == username).SelectMany(x => x.ApplicationUserGroups).Select(x => x.Group)
                .SelectMany(x => x.GroupRoles).Select(x => x.Role).SelectMany(x => x.PermissionRoles)
                .Select(x => x.Permission.Name).ToList();
            return result;
        }

        public bool IsDefaultUserWithId(string userId)
        {
            return IsExist(o => o.IsDefault == true && o.Id == userId);
        }

        public bool IsDefaultUserWithUserName(string userName)
        {
            return IsExist(o => o.IsDefault == true && o.UserName == userName);
        }

        public ResultApiModel<IEnumerable<UsersModel>> GetUsers(UserFilterModel filter)
        {
            var users = dbSet.Where(d => d.UserType != PredefinedUserType.DRIVER && d.UserType != PredefinedUserType.DISTRIBUTOR).Where(x => (x.UserType == (PredefinedUserType)filter.userType || filter.userType == 0)
              &&
              (
              x.FirstName.Contains(filter.userName) ||
              x.LastName.Contains(filter.userName) ||
              (x.FirstName + " " + x.LastName).Contains(filter.userName) ||
              x.UserName.Contains(filter.userName) || filter.userName == null || filter.userName == string.Empty)
              &&
              (x.Email.Contains(filter.email) || filter.email == null || filter.email == string.Empty)
              &&
              (x.Phone.Contains(filter.phone) || filter.phone == null || filter.phone == string.Empty)
            ).Select(u => new UsersModel()
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                CreationDate = u.CreationDate,
                Phone = u.Phone,
                UserType = u.UserType
            });
            return this.Paginate(users, filter.page, filter.take);
        }

        public ResultApiModel<IEnumerable<DistributorModel>> GetDistributors(DistributorFilterModel filter)
        {
            var users = dbSet.Include(d => d.DistributionPoint).Where(d => d.UserType == PredefinedUserType.DISTRIBUTOR)
            .Where(x => ((x.FirstName + " " + x.LastName).Contains(filter.Name) || x.FirstName.Contains(filter.Name) || x.LastName.Contains(filter.Name) || filter.Name == null || filter.Name == string.Empty)
            &&
            ((x.IdentityNumber != null && x.IdentityNumber.Contains(filter.IdentityNumber)) || filter.IdentityNumber == null || filter.IdentityNumber == string.Empty)
            &&
            ((x.Code != null && x.Code.Contains(filter.Code)) || filter.Code == null || filter.Code == string.Empty)
            &&
            ((x.BirthDate != null && x.BirthDate.Value.Date == filter.BirthDate) || filter.BirthDate == null)
            &&
            ((x.NominationDate != null && x.NominationDate.Value.Date == filter.NominationDate) || filter.NominationDate == null)
            &&
            (x.DistributionPointId == filter.DistributionPointId || filter.DistributionPointId == null)
            ).Select(u => new DistributorModel()
            {
                Id = u.Id,
                Code = u.Code,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                CreationDate = u.CreationDate,
                Phone = u.Phone,
                Phone2 = u.Phone2,
                BirthDate = u.BirthDate,
                IdentityExpiration = u.IdentityExpiration,
                IdentityNumber = u.IdentityNumber,
                Lang = u.Lang,
                NominationDate = u.NominationDate,
                DistributionPointName = u.DistributionPointId != null ? u.DistributionPoint.Name : "",
                DistributionPointId = u.DistributionPointId,
                Inventory = context.DistributorInventories.Where(d => d.DistributorId == u.Id).Sum(s => s.AddedQuantity - s.ConsumedQuantity),
            });
            return this.Paginate(users, filter.page, filter.take);
        }


        public ResultApiModel<IEnumerable<DriverModel>> GetDrivers(DriverFilterModel filter)
        {
            var users = dbSet.Include(d => d.Country).Where(d => d.UserType == PredefinedUserType.DRIVER)
            .Where(x => ((x.FirstName + " " + x.LastName).Contains(filter.Name) || x.FirstName.Contains(filter.Name) || x.LastName.Contains(filter.Name) || filter.Name == null || filter.Name == string.Empty)
            &&
            ((x.IdentityNumber != null && x.IdentityNumber.Contains(filter.IdentityNumber)) || filter.IdentityNumber == null || filter.IdentityNumber == string.Empty)
            &&
            ((x.Code != null && x.Code.Contains(filter.Code)) || filter.Code == null || filter.Code == string.Empty)
            &&
            ((x.BirthDate != null && x.BirthDate.Value.Date == filter.BirthDate) || filter.BirthDate == null)
            ).Select(u => new DriverModel()
            {
                Id = u.Id,
                Code = u.Code,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                CreationDate = u.CreationDate,
                Phone = u.Phone,
                Phone2 = u.Phone2,
                BirthDate = u.BirthDate,
                IdentityExpiration = u.IdentityExpiration,
                IdentityNumber = u.IdentityNumber,
                Lang = u.Lang,
                DrivingLicense = u.DrivingLicense,
                DrivingLicenseExpiration = u.DrivingLicenseExpiration,
                CountryName = u.Country != null ? u.Country.Name : ""
            });
            return this.Paginate(users, filter.page, filter.take);
        }

        public ApplicationUser GetDetailsById(object id)
        {
            return dbSet.Include(x=>x.Center).Include(x => x.DistributionPoint).FirstOrDefault(x => x.Id == id);
        }

    }
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetByUserName(string userName);
        ApplicationUser GetByEmail(string Email);
        bool IsExistUserName(string userName, int? zoneId);
        List<string> GetUserPermissions(string username);
        bool IsDefaultUserWithId(string userId);
        bool IsDefaultUserWithUserName(string userName);
        ResultApiModel<IEnumerable<UsersModel>> GetUsers(UserFilterModel filter);
        ResultApiModel<IEnumerable<DistributorModel>> GetDistributors(DistributorFilterModel filter);
        ResultApiModel<IEnumerable<DriverModel>> GetDrivers(DriverFilterModel filter);
        ApplicationUser GetDetailsById(object id);
        DistributorStats GetDistributorStatistics(string userId);
    }
}
