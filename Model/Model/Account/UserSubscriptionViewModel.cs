using System;

namespace FTS.Model.Account
{
    public class UserSubscriptionViewModel
    {
        public int SubscriptionId { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsAllowDelete { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}