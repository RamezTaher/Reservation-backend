using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamazimah.Core.Utils
{
    public static class HijriCalendarHelper
    {
        public static DateTime? ConvertFromHijriToGregorian(string hijriDate, string format)
        {
            if (string.IsNullOrEmpty(hijriDate))
            {
                return null;
            }
            try
            {
                CultureInfo arSA = new CultureInfo("ar-SA");
                arSA.DateTimeFormat.Calendar = new HijriCalendar();
                var dateValue = DateTime.ParseExact(hijriDate, format, arSA);
                return dateValue;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
