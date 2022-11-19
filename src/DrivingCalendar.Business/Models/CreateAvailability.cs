using System;

namespace DrivingCalendar.Business.Models
{
    public class CreateAvailability
    {
        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
