using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DrivingCalendar.Business.Models;

namespace DrivingCalendar.Infrastructure.Entities
{
    [Table("StudentInstructors")]
    internal class StudentInstructorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
