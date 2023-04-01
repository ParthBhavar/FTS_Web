namespace Identity.Model
{
    using System;

    public class ApplicationUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTime? LockoutEndDateTimeUtc { get; set; }

        public int AccessFailedCount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }              
        public int CurrentRole { get; set; }

        public string Token { get; set; }

    }
}
