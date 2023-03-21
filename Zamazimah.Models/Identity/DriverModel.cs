using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Models.Identity
{
    public class DriverModel
    {
        public string Id { get; set; }
        public string? Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string? Phone2 { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Lang { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? IdentityExpiration { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string? DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpiration { get; set; }
        public string CountryName { get; set; }
    }
}
