using DrivingCalendar.Business.Constants;
using System.ComponentModel.DataAnnotations;

namespace DrivingCalendar.API.Models
{
    public class PatchDrivingLessonStatusRequest
    {
        [Required]
        public DrivingLessonStatus? Status { get; set; }
    }
}
