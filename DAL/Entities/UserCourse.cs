using DAL.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class UserCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? ReviewText { get; set; }
        public Stars? SEvaluation { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;    ///Sould calculated on DB
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Course Course { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
    }
}
