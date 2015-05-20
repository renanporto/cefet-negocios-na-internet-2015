using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyStore.WebApp.Util
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertFromUtc(this DateTime from)
        {
            var zone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var fromUtc = TimeZoneInfo.ConvertTimeFromUtc(from, zone);
            return fromUtc;
        }
    }
}