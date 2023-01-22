namespace DrivingCalendar.API.Models
{
    public class InstructorResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public CompanyResponse Company { get; set; }
    }
}
