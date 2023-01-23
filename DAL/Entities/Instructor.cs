using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string About { get; set; }
        public string JopTitle { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

    }
}
