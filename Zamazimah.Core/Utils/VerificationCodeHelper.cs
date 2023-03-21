using System;
using System.Collections.Generic;
using System.Text;

namespace Zamazimah.Core.Utils
{
    public static class VerificationCodeHelper
    {
        public static string GenerateRandomDigits(int length = 6)
        {
            Random generator = new Random();
            if(length == 4)
            {
                return generator.Next(0, 10000).ToString("D4");
            }
            return generator.Next(0, 1000000).ToString("D6");
        }
    }
}
