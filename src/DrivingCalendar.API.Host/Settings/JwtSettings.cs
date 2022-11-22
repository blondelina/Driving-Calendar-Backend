﻿namespace DrivingCalendar.API.Host.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public int ExpirationInHours { get; set; }
    }
}
