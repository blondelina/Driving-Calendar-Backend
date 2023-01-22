using System;

namespace DrivingCalendar.API.Models
{
    public class StudentDetailResponse : StudentResponse
    {
        public DateTime? ExamDate { get; set; }
    }
}
