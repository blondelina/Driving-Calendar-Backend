using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DrivingCalendar.Business.Constants;
using DrivingCalendar.Business.Models;

namespace DrivingCalendar.Infrastructure.Entities
{
    [Table("DrivingLessons")]

    internal class DrivingLessonEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int InstructorId { get; set; }

        public int StudentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndTime { get; set; }

        public DrivingLessonStatus StudentStatus { get; set; }

        public DrivingLessonStatus InstructorStatus { get; set; }
    }
}
