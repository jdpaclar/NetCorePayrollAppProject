using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Common.Utilities
{
    public static class StringExtensions
    {
        public static string FormatString(this string format, params object[] args)
        {
            var formattedString = string.Format(format, args);
            return formattedString;
        }

        public static decimal ToDecimalFromPercentage(this string vPercentValue)
        {
            return decimal.Parse(vPercentValue.Substring(0, vPercentValue.Length - 1)) / 100;
        }

        public static string ToMonthPeriodFormat(this DateTime pDateInput)
        {
            if (pDateInput == null)
                return "";

            var firstDayOfMonth = new DateTime(pDateInput.Year, pDateInput.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            return "{0} {1}".FormatString(firstDayOfMonth.Day, lastDayOfMonth.Day);
        }

        public static bool IsValidDate(this string pDate)
        {
            return DateTime.TryParseExact(pDate, "dd MMMM", null, System.Globalization.DateTimeStyles.None, out DateTime dFormattedDate);
        }
    }
}
