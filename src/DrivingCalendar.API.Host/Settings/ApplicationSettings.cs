namespace DrivingCalendar.API.Host.Settings
{
    public class ApplicationSettings
    {
        public ConnectionStringSettings ConnectionStrings { get; set; } = new();
        public JwtSettings JwtSettings { get; set; } = new();
    }
}
