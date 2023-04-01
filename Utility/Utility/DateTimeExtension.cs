namespace FTS.Extensions
{
    using System;
    using NodaTime;
      
    public static class DateTimeExtension
    {
        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="timeZoneId">The time zone identifier.</param>
        /// <returns>
        /// Date time
        /// </returns>
        public static DateTime ToTimeZoneTime(this DateTime time, string timeZoneId = "Pacific Standard Time")
        {
            try
            {
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return time.ToTimeZoneTime(tzi);
            }
            catch
            {
                var easternTimeZone = DateTimeZoneProviders.Tzdb[timeZoneId];
                return Instant.FromDateTimeUtc(time)
                    .InZone(easternTimeZone)
                    .ToDateTimeUnspecified();
            }
        }

        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time">The  date time.</param>
        /// <param name="tzi">The time zone info.</param>
        /// <returns>Date time</returns>
        public static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo tzi)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
        }

        public static DateTime ToUTC(this DateTime dateTime, int offset)
        {
            TimeSpan timespan = TimeSpan.FromMinutes(offset);
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, timespan);
            return dateTimeOffset.ToUniversalTime().DateTime;
        }

        public static DateTime FromUTC(this DateTime dateTime, int offset)
        {
            TimeSpan timespan = TimeSpan.FromMinutes(offset);
            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, timespan);
            return dateTimeOffset.DateTime;
        }
    }
}
