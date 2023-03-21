using System;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class UserRoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsChecked { get; set; }
    }


    public class GetUserModel
    {
        public string Id { get; set; }
        public string? Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string? Phone2 { get; set; }
        public string Email { get; set; }
        public string? Position { get; set; }

        public string UserName { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? Lang { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? IdentityExpiration { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? NominationDate { get; set; }
        public PredefinedUserType UserType { get; set; }
        public string? DrivingLicense { get; set; }
        public DateTime? DrivingLicenseExpiration { get; set; }
        public string? DistributionPointName { get; set; }
        public int? DistributionPointId { get; set; }

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
        public string? CenterName { get; set; }
    }
    public class UsersModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public PredefinedUserType UserType { get; set; }
        public int Order { get; set; }
    }
    public class AgencyUserModel
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class UpdateProfileModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
    public class ConfirmEmailModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string CodeEmail { get; set; }
        public string CodePhone { get; set; }
    }
    public class RegisterModel
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Agency
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CaptchaToken { get; set; }
    }

    public class AuthenticateRequest
    {
        public string IdToken { get; set; }
    }
    public class UserTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class OTP_Validation
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
    public class OTP_Resend
    {
        public string UserName { get; set; }
    }
    public class GoogleCaptchaResponse
    {
        public bool success;
        public double score;
    }

}
