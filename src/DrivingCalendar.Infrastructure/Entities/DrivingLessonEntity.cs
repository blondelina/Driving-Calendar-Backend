using DrivingCalendar.Business.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingCalendar.Infrastructure.Entities
{
    [Table("DrivingLesson")]

    internal class DrivingLessonEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(BaseUserEntity))]
        public string InstructorId { get; set; }

        [ForeignKey(nameof(BaseUserEntity))]
        public string StudentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndTime { get; set; }

        public DrivingLessonStatus StudentStatus { get; set; }

        public DrivingLessonStatus InstructorStatus { get; set; }
    }
}
