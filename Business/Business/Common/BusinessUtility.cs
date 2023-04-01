using System;
using System.Globalization;
using static FTS.Model.Constants.Constants;

namespace FTS.Business.Common
{
    public static class BusinessUtility
    {
        public static bool IsOrganizationTimeFreezed(string deliveryTime, DateTime orderTime, int freezingDuration)
        {
            DateTime orgFreezingTime = DateTime.ParseExact(deliveryTime, DateTimeConstants.HHmm,
                                            CultureInfo.InvariantCulture);

            orderTime = orderTime.AddMinutes(freezingDuration);

            return orgFreezingTime < orderTime;
        }

        public static DateTime GetOrgFreezingDate(string deliveryTime, DateTime deliveryDate, int offset)
        {
            DateTime orgDeliveryTime = DateTime.ParseExact(deliveryTime, DateTimeConstants.HHmm,
                                    CultureInfo.InvariantCulture);

            DateTime orderFreezingDateTime = deliveryDate.Date.AddMinutes(orgDeliveryTime.TimeOfDay.TotalMinutes);

            //return deliveryDate.Date.AddMinutes(orgDeliveryTime.TimeOfDay.TotalMinutes);

            DateTime offsetDateTime = orderFreezingDateTime.AddMinutes(offset);

            TimeSpan maxTime = new TimeSpan(23, 59, 59);

            var freezingDate = orderFreezingDateTime.Date != offsetDateTime.Date
                ? deliveryDate.AddDays(-1).Date
                : deliveryDate.Date;

            return freezingDate.Date.AddMinutes(orgDeliveryTime.TimeOfDay.TotalMinutes);
        }
    }
}
