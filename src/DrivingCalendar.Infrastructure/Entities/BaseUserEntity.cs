using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingCalendar.Infrastructure.Entities
{
    [Table("BaseUser")]
    public class BaseUserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        
        public string Salt { get; set; }

        public string Email { get; set; }
    }
}
