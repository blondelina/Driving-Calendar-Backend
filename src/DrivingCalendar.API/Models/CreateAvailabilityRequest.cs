using System;
using System.ComponentModel.DataAnnotations;

namespace DrivingCalendar.API.Models
{
    public class CreateAvailabilityRequest
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int Repeat { get; set; } 
    }
}
