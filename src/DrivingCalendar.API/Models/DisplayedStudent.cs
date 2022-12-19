using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCalendar.API.Models
{
    public class DisplayedStudent
    {
        [Required]
        public int studentId { get; set; }
        [Required]
        public string studentUserName { get; set; }
    }
}
