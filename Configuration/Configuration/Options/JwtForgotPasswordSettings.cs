﻿namespace FTS.Configuration.Options
{
    public class JwtForgotPasswordSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public int ExpirationTimeInMinute { get; set; }
    }
}
