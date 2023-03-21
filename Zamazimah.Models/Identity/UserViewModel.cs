using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models.Identity
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Position { get; set; }
        public string Caisse { get; set; }

        public bool IsAdmin { get; set; }
        //public UserType UserType { get; set; }
        public int? ZoneId { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class CreateUserViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; } 
        public string? Phone2 { get; set; }

        public string? Position { get; set; }

        public int? CountryId { get; set; }
        public string? Lang { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? IdentityExpiration { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? NominationDate { get; set; }
        public PredefinedUserType UserType { get; set; }
        public string? DrivingLicense { get; set; }
        public DateTime DrivingLicenseExpiration { get; set; }
        public int? DistributionPointId { get; set; }
        public int? HousingContractId { get; set; }
        public string? Code { get; set; }
        // vice representative 1
        public string? ViceFirstName { get; set; }
        public string? ViceLastName { get; set; }
        public string? ViceEmail { get; set; }
        public string? VicePhone { get; set; }

        // vice representative 2
        public string? ViceFirstName2 { get; set; }
        public string? ViceLastName2 { get; set; }
        public string? ViceEmail2 { get; set; }
        public string? VicePhone2 { get; set; }

        // vice representative 3
        public string? ViceFirstName3 { get; set; }
        public string? ViceLastName3 { get; set; }
        public string? ViceEmail3 { get; set; }
        public string? VicePhone3 { get; set; }
        public int? CenterId { get; set; }

    }

    public class UserGroupEntryViewModel
    {
        public string UserId { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string UserName { get; set; }

        public bool IsSelected { get; set; }
    }

    public class UserApiModel
    {
        public List<UserViewModel> Data { get; set; }
        public bool Success { get; set; }
        public int TotalCount { get; set; }

    }
}
