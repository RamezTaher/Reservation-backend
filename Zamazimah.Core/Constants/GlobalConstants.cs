using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Core.Constants
{
    public class Langs
    {
        public const string AR = "ar";
        public const string EN = "en";
    }

    public class ValidationsErrorCodes
    {
        public const int UserNameExistCode = -1;
        public const string UserNameExistMessage = "اسم المستخدم مسجل من قبل ";

        public const int EmailExistCode = -2;
        public const string EmailExistMessage = "البريد الالكتروني مسجل من قبل";

        public const int PhoneExistCode = -3;
        public const string PhoneExistMessage = "الجول مسجل من قبل";

        public const int ConfirmationCodeNotExistCode = -4;
        public const string ConfirmationCodeNotExistMessage = "code not exist";

        public const int VerificationCodeNotExist = -5;
        public const string VerificationCodeNotExistMessage = "verification code not exist";
    }
    public class PicturesFolder
    {
        public const string HousingContractsFolder = "HousingContracts";
    }
}
