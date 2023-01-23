using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CourseReview
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string TEvaluation { get; set; } //=All SEvaluation/Count(courseid)
        public string SEvaluation { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;  ///Sould calculated on DB
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Course Course { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }



    }
}
