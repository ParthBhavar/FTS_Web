namespace FTS.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NodaTime.TimeZones;

    public static class Utility
    {
        /// <summary>
        /// Gets the random color of the hexadecimal.
        /// </summary>
        /// <returns>return random color hex string</returns>
        public static string GetRandomHexColor()
        {
            var random = new Random();
            var color = string.Format("#{0:X6}", random.Next(0x1000000));
            return color;
        }

        /// <summary>
        /// Formats the price to specified culture
        /// </summary>
        /// <param name="price">The price.</param>
        /// <returns>Formatted price as per culture specified in Startup file</returns>
        public static string FormatPrice(decimal? price)
        {
            return string.Format("{0:C}", price ?? 0);
        }

        /// <summary>
        /// Calculates the percentage.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="total">The total.</param>
        /// <returns>Calculated Percentage</returns>
        public static decimal CalculatePercentage(decimal value, decimal total)
        {
            if (total == 0)
                return 100;
            return (value / total) * 100;
        }

        /// <summary>
        /// Calculates the time difference between two zone.
        /// </summary>
        /// <param name="zone1">The zone1.</param>
        /// <param name="zone2">The zone2.</param>
        /// <returns>Difference in hours</returns>
        public static double CalculateTimeDifferenceBetweenTwoZone(string zone1, string zone2)
        {
            DateTime time1 = DateTime.UtcNow.ToTimeZoneTime(zone1);
            DateTime time2 = DateTime.UtcNow.ToTimeZoneTime(zone2);
            return Math.Abs(time1.Subtract(time2).TotalHours);
        }

        public static string GetTimeZoneByCountryCode(string countryCode)
        {
            return (TzdbDateTimeZoneSource.Default.ZoneLocations ?? throw new InvalidOperationException())
                 .Where(x => x.CountryCode.ToLower() == countryCode)
                 .Select(x => x.ZoneId).FirstOrDefault();
        }

        public static string GetTimeBetweenTwoDate(DateTime fromDate, DateTime toDate)
        {
            string timeString = null;

            TimeSpan timeSpan = toDate.Subtract(fromDate);
            var totalMinute = timeSpan.TotalMinutes;
            var totalHours = totalMinute / 60;
            totalMinute = totalMinute % 60;

            if (totalHours < 24)
            {
                if (totalHours >= 1)
                    timeString += " " + Math.Round(totalHours) + " hours";
                if (totalMinute >= 1)
                    timeString += " " + Math.Round(totalMinute) + " min";
                if (!string.IsNullOrEmpty(timeString))
                    timeString += " ago";
            }
            else
            {
                timeString = fromDate.ToString("dddd, dd MMMM yyyy hh:mm tt");
            }

            return timeString;
        }

        public static string GetSortedString (string commaSepratedString)
        {
            List<string> uniques = commaSepratedString.Split(',').Distinct().ToList();
            return string.Join(",", uniques);
        }
    }
}
