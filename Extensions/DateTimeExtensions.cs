using System;

namespace Extensions
{
    public static class DateTimeExtensions
    {
        public static int Calculateage(this DateTime dt)
        {
            var today = DateTime.Today;
            var age = today.Year - dt.Year;
            if (dt.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}