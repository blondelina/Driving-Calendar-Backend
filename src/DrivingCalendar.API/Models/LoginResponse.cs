namespace DrivingCalendar.API.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public long UnixTimeExpiresAt { get; set; }
    }
}
