namespace DrivingCalendar.API.Options
{
    public class ApiOptions
    {
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string JwtSecretKey { get; set; }
        public int JwtExpirationInHours { get; set; }
    }
}
