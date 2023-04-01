using FTS.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace FTS.Model.Account
{
    public class UserSubscription : BaseEntity
    {
        public int SubscriptionId { get; set; }

        [Required(ErrorMessage = "UserIdRequired")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "StartDateRequired")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDateRequired")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "TimezoneOffsetRequired")]
        public int TimezoneOffset { get; set; }
    }
}