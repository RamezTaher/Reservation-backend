using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Entities.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ApplicationUserGroups = new List<ApplicationUserGroup>();
            ApplicationUserRoles = new List<ApplicationUserRole>();
            IsDefault = false;
            CreationDate = DateTime.Now;
        }
        public string? Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public string Phone { get; set; }
        public string? Phone2 { get; set; }
        public string? Position { get; set; }
        public bool Disabled { get; set; }
        public bool IsDefault { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public DateTime CreationDate { get; set; }
        public string? PhoneVerificationCode { get; set; }
        public string? EmailVerificationCode { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string? Lang { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? IdentityExpiration { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? NominationDate { get; set; }
        public PredefinedUserType UserType { get; set; }
        public string? DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpiration { get; set; }

        public int? DistributionPointId { get; set; }
        public virtual DistributionPoint DistributionPoint { get; set; }

        // vice representative
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
        public virtual Center Center { get; set; }
    }
}
