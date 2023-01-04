using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCalendar.API.Models
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set;}
    }
}
