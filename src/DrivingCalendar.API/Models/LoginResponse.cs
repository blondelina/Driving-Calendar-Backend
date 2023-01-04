namespace DrivingCalendar.API.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public long UnixTimeExpiresAt { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
