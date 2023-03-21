using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using Zamazimah.Core.Utils;
using Zamazimah.Data.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Generic.Models;
using Zamazimah.Models;
using Zamazimah.Models.DistributionCycles;
using Zamazimah.Models.Identity;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUserRepository userRepository;
        private readonly IHousingContractRepository _housingContractRepository;
        private readonly IDistributorInventoryRepository _distributorInventoryRepository;
        private readonly IDistributionCycleRepository _distributionCycleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;


        public UserService(IApplicationUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository,
            IHousingContractRepository housingContractRepository,
            IDistributionCycleRepository distributionCycleRepository,
            IDistributorInventoryRepository distributorInventoryRepository,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            this.userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _userManager = userManager;
            _housingContractRepository = housingContractRepository;
            _distributionCycleRepository = distributionCycleRepository;
            _distributorInventoryRepository = distributorInventoryRepository;
            _configuration = configuration;
        }

        public ApplicationUser GetByUserName(string username)
        {
            return userRepository.GetByUserName(username);
        }

        public DistributorStats GetDistributorStatistics(string userId)
        {
            return userRepository.GetDistributorStatistics(userId);
        }

        public void ActivateAccount(ApplicationUser user)
        {
            user.Disabled = false;
            userRepository.SaveChanges();
        }

        public void ConfirmEmail(ApplicationUser user)
        {
            user.EmailConfirmed = true;
            userRepository.SaveChanges();
        }
        public void ConfirmPhone(ApplicationUser user)
        {
            user.PhoneNumberConfirmed = true;
            userRepository.SaveChanges();
        }

        public ApplicationUser GetById(string id)
        {
            return userRepository.GetById(id);
        }


        public GetUserModel GetUserModelById(string id)
        {
            var user = userRepository.GetDetailsById(id);
            if (user == null) return null;
            var userModel = new GetUserModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Position = user.Position,
                UserName = user.UserName,
                BirthDate = user.BirthDate,
                CountryId = user.CountryId,
                DrivingLicense = user.DrivingLicense,
                DrivingLicenseExpiration = user.DrivingLicenseExpiration,
                IdentityExpiration = user.IdentityExpiration,
                IdentityNumber = user.IdentityNumber,
                Lang = user.Lang,
                NominationDate = user.NominationDate,
                Phone2 = user.Phone2,
                UserType = user.UserType,
                Code = user.Code,
                CountryName = user.Country != null ? user.Country.Name : "",
                DistributionPointName = user.DistributionPoint != null ? user.DistributionPoint.Name : "",
                DistributionPointId = user.DistributionPointId,
                // representative 1
                ViceEmail = user.ViceEmail,
                ViceFirstName = user.ViceFirstName,
                ViceLastName = user.ViceLastName,
                VicePhone = user.VicePhone,
                // representative 2
                ViceFirstName2 = user.ViceFirstName2,
                ViceLastName2 = user.ViceLastName2,
                ViceEmail2 = user.ViceEmail2,
                VicePhone2 = user.VicePhone2,
                // representative 3
                ViceFirstName3 = user.ViceFirstName3,
                ViceLastName3 = user.ViceLastName3,
                ViceEmail3 = user.ViceEmail3,
                VicePhone3 = user.VicePhone3,
                CenterId = user.CenterId,
                CenterName = user.Center != null ? user.Center.Name : "",
            };
            return userModel;
        }

        public int Update(ApplicationUser oldUser, CreateUserViewModel model)
        {
            oldUser.FirstName = model.FirstName;
            oldUser.LastName = model.LastName;
            oldUser.Phone = model.Phone;
            oldUser.Phone2 = model.Phone2;
            oldUser.Position = model.Position;
            oldUser.Email = model.Email;
            oldUser.CountryId = model.CountryId;
            oldUser.Lang = model.Lang;
            //  oldUser.IdentityNumber = model.IdentityNumber;
            oldUser.IdentityExpiration = model.IdentityExpiration;
            oldUser.BirthDate = model.BirthDate;
            oldUser.NominationDate = model.NominationDate;
            oldUser.DrivingLicense = model.DrivingLicense;
            oldUser.DrivingLicenseExpiration = model.DrivingLicenseExpiration;
            // representative 1
            oldUser.ViceFirstName = model.ViceFirstName;
            oldUser.ViceLastName = model.ViceLastName;
            oldUser.ViceEmail = model.ViceEmail;
            oldUser.VicePhone = model.VicePhone;
            // representative 2
            oldUser.ViceFirstName2 = model.ViceFirstName2;
            oldUser.ViceLastName2 = model.ViceLastName2;
            oldUser.ViceEmail2 = model.ViceEmail2;
            oldUser.VicePhone2 = model.VicePhone2;
            // representative 3
            oldUser.ViceFirstName3 = model.ViceFirstName3;
            oldUser.ViceLastName3 = model.ViceLastName3;
            oldUser.ViceEmail3 = model.ViceEmail3;
            oldUser.VicePhone3 = model.VicePhone3;
            //    oldUser.DistributionPointId = model.DistributionPointId;
            oldUser.Code = model.Code;
            oldUser.CenterId = model.CenterId;
            if (oldUser.UserType == PredefinedUserType.ADMIN
                || oldUser.UserType == PredefinedUserType.REPRESENTATIVE
                || oldUser.UserType == PredefinedUserType.SUPERVISOR
                || oldUser.UserType == PredefinedUserType.SUPERVISOR_REPORTS)
            {
                oldUser.UserType = model.UserType;
            }
            int rows = userRepository.SaveChanges();
            return rows;
        }


        public int Delete(ApplicationUser user)
        {
            if (!_housingContractRepository.IsExist(x => x.ResponsableId == user.Id) &&
                !_distributorInventoryRepository.IsExist(x => x.DistributorId == user.Id) &&
                !_distributionCycleRepository.IsExist(x => x.DriverId == user.Id || x.DistributorId == user.Id)
                )
            {
                userRepository.Remove(user);
                int rows = userRepository.SaveChanges();
                return rows;
            }
            else
            {
                return -1;
            }
        }

        public int Insert(ApplicationUser user)
        {
            userRepository.Insert(user);
            userRepository.SaveChanges();

            return 0;
        }

        public bool IsExistUserName(string userName, int? ZoneId)
        {
            bool isExist = userRepository.IsExistUserName(userName, ZoneId);
            return isExist;
        }

        public bool IsUserHasPermission(string userName, string permission)
        {
            var permissions = userRepository.GetUserPermissions(userName);
            if (permissions.Count() > 0)
            {
                return permissions.Contains(permission);
            }
            return false;
        }

        public List<string> GetUserPermissions(string userName)
        {
            var permissions = userRepository.GetUserPermissions(userName);
            return permissions;
        }

        public bool IsDefaultUserWithId(string userId)
        {
            bool exist = userRepository.IsDefaultUserWithId(userId);
            return exist;
        }

        public bool IsDefaultUserWithUserName(string userName)
        {
            bool exist = userRepository.IsDefaultUserWithUserName(userName);
            return exist;
        }

        public void SetEmailCode(ApplicationUser user, string code)
        {
            user.EmailVerificationCode = code;
            _userRoleRepository.SaveChanges();
        }
        public bool IsExist(Expression<Func<ApplicationUser, bool>> filter)
        {
            return userRepository.IsExist(filter);
        }
        public ApplicationUser GetByEmail(string email)
        {
            return userRepository.GetByEmail(email);
        }

        public List<UserRoleModel> GetUserRoles(int officeId, string userId)
        {
            return _roleRepository.GetUserRoles(officeId, userId);
        }

        public void ToggleManyRoles(int officeId, string id, List<int> roles)
        {


            var userRoles = _roleRepository.GetCheckedUserRoles(officeId, id);
            var userRolesToInsert = new List<ApplicationUserRole>();
            var userRolesToDelete = new List<ApplicationUserRole>();

            foreach (int role in roles)
            {
                if (userRoles.Any(x => x.RoleId == role && !x.IsChecked))
                {
                    userRolesToInsert.Add(new ApplicationUserRole { ApplicationUserId = id, RoleId = role });
                }
                if (userRoles.Any(x => x.RoleId == role && x.IsChecked))
                {
                    userRolesToDelete.Add(new ApplicationUserRole { ApplicationUserId = id, RoleId = role });
                }
            }
            if (userRolesToInsert.Count() > 0)
            {
                _userRoleRepository.InsertMultiple(userRolesToInsert);
            }
            if (userRolesToDelete.Count() > 0)
            {
                _userRoleRepository.RemoveMany(userRolesToDelete);
            }
            int rows = _roleRepository.SaveChanges();
        }

        public ResultApiModel<IEnumerable<UsersModel>> GetUsers(UserFilterModel filter)
        {
            var users = userRepository.GetUsers(filter);
            return users;
        }
        public ResultApiModel<IEnumerable<DistributorModel>> GetDistributors(DistributorFilterModel filter)
        {
            var users = userRepository.GetDistributors(filter);
            return users;
        }

        public ResultApiModel<IEnumerable<DriverModel>> GetDrivers(DriverFilterModel filter)
        {
            var users = userRepository.GetDrivers(filter);
            return users;
        }

        public List<UserTypeModel> GetPredefinedUserTypes()
        {
            var results = new List<UserTypeModel>();
            results.Add(new UserTypeModel
            {
                Id = (int)PredefinedUserType.ADMIN,
                Name = PredefinedUserType.ADMIN.ToString()
            });
            results.Add(new UserTypeModel
            {
                Id = (int)PredefinedUserType.REPRESENTATIVE,
                Name = PredefinedUserType.REPRESENTATIVE.ToString()
            });
            results.Add(new UserTypeModel
            {
                Id = (int)PredefinedUserType.DISTRIBUTOR,
                Name = PredefinedUserType.DISTRIBUTOR.ToString()
            });
            results.Add(new UserTypeModel
            {
                Id = (int)PredefinedUserType.PLANNER,
                Name = PredefinedUserType.PLANNER.ToString()
            });
            results.Add(new UserTypeModel
            {
                Id = (int)PredefinedUserType.DRIVER,
                Name = PredefinedUserType.DRIVER.ToString()
            });
            return results;
        }

        public List<UsersModel> AutoComplete(ApplicationUser user, string query, PredefinedUserType userType = 0, int entryPointId = 0)
        {
            var users = userRepository.Get(x =>
            (x.FirstName.Contains(query) || x.LastName.Contains(query) || query == null || query == "")
            && (x.UserType == userType || userType == 0)
            && (x.DistributionPointId == entryPointId || entryPointId == 0)

            ).Take(100).Select(u => new UsersModel
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                CreationDate = u.CreationDate,
                Phone = u.Phone,
                UserType = u.UserType,
                Order = user.CenterId == u.CenterId ? 1 : 2,
            }).OrderBy(x => x.Order).ToList();
            return users;
        }

        public ImportResult ImportDistributorsFromExcel(IFormFile file)
        {
            try
            {
                var fileextension = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "files", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                int rowno = 0;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                foreach (var row in rows)
                {
                    if (rowno > 1)
                    {
                        var number = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(number) || string.IsNullOrEmpty(number))
                        {
                            break;
                        }
                        var first_name = row.Cell(2).Value.ToString();
                        var last_name = row.Cell(3).Value.ToString();
                        var nationality = row.Cell(4).Value.ToString();
                        var identity = row.Cell(5).Value.ToString();
                        var identity_expiration = row.Cell(6).Value.ToString();
                        var birthday = row.Cell(7).Value.ToString();
                        var nomination_date = row.Cell(8).Value.ToString();
                        var phone = row.Cell(9).Value.ToString();
                        var email = row.Cell(10).Value.ToString();
                        var existedUser = userRepository.GetByEmail(email);
                        if (existedUser == null)
                        {
                            var user = new ApplicationUser
                            {
                                FirstName = first_name,
                                LastName = last_name,
                                Phone = "966" + phone,
                                Email = email,
                                UserName = identity,
                                IdentityNumber = identity,
                                CountryId = 835,
                                UserType = PredefinedUserType.DISTRIBUTOR,
                                IdentityExpiration = HijriCalendarHelper.ConvertFromHijriToGregorian(identity_expiration, "yyyy/MM/dd"),
                                BirthDate = HijriCalendarHelper.ConvertFromHijriToGregorian(birthday, "yyyy/MM/dd"),
                                NominationDate = HijriCalendarHelper.ConvertFromHijriToGregorian(birthday, "dd-MM-yyyy"),
                                DistributionPointId = 1,
                                Lang = "ar",
                                EmailConfirmed = true,
                                Position = "موزع"
                            };
                            var result = _userManager.CreateAsync(user, "0" + phone).Result;
                            if (result.Succeeded)
                            {

                            }
                        }
                        else
                        {
                            existedUser.FirstName = first_name;
                            existedUser.LastName = last_name;
                            existedUser.IdentityNumber = identity;
                            existedUser.IdentityExpiration = HijriCalendarHelper.ConvertFromHijriToGregorian(identity_expiration, "yyyy/MM/dd");
                            existedUser.BirthDate = HijriCalendarHelper.ConvertFromHijriToGregorian(birthday, "yyyy/MM/dd");
                            existedUser.NominationDate = HijriCalendarHelper.ConvertFromHijriToGregorian(birthday, "dd-MM-yyyy");
                            userRepository.SaveChanges();
                        }
                    }
                    rowno++;
                }
                return new ImportResult { success = true, description = "success" };
            }
            catch (Exception ex)
            {
                return new ImportResult { success = false, description = ex.Message + ex.InnerException + ex.StackTrace };
            }
        }

        public async Task<bool> VerifyCaptchaToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_configuration["GoogleRecaptchaSecretKey"]}&response={token}";
                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if (httpResult.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    var responseString = await httpResult.Content.ReadAsStringAsync();
                    var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString); ;
                    return googleResult.success && googleResult.score >= 0.5;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
    public interface IUserService
    {
        ApplicationUser GetByEmail(string email);
        ApplicationUser GetByUserName(string username);
        int Delete(ApplicationUser user);
        int Update(ApplicationUser oldUser, CreateUserViewModel model);
        int Insert(ApplicationUser user);
        ApplicationUser GetById(string id);
        bool IsExistUserName(string userName, int? ZoneId);
        bool IsUserHasPermission(string userName, string permission);
        List<string> GetUserPermissions(string userName);
        bool IsDefaultUserWithId(string userId);
        bool IsDefaultUserWithUserName(string userName);
        bool IsExist(Expression<Func<ApplicationUser, bool>> filter);
        List<UserRoleModel> GetUserRoles(int officeId, string userId);
        void ToggleManyRoles(int officeId, string id, List<int> roles);
        GetUserModel GetUserModelById(string id);
        void ActivateAccount(ApplicationUser user);
        ResultApiModel<IEnumerable<UsersModel>> GetUsers(UserFilterModel filter);
        void ConfirmEmail(ApplicationUser user);
        void ConfirmPhone(ApplicationUser user);
        void SetEmailCode(ApplicationUser user, string code);
        List<UserTypeModel> GetPredefinedUserTypes();
        List<UsersModel> AutoComplete(ApplicationUser user, string query, PredefinedUserType userType = 0, int entryPointId = 0);
        ResultApiModel<IEnumerable<DistributorModel>> GetDistributors(DistributorFilterModel filter);
        ResultApiModel<IEnumerable<DriverModel>> GetDrivers(DriverFilterModel filter);
        ImportResult ImportDistributorsFromExcel(IFormFile file);
        Task<bool> VerifyCaptchaToken(string token);
        DistributorStats GetDistributorStatistics(string userId);
    }

    public class ImportResult
    {
        public bool success { get; set; }
        public string description { get; set; }
    }

}
