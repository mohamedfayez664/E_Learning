using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        //public Media PublicUrl { get; set; }
        //[ForeignKey("Media")]
        //public int MediaId { get; set; }
        public decimal? TEvaluation { get; set; }    //=All SEvaluation/Count(courseid) from usercourse
        public string? ImageUrl { get; set; }
        public string? PdfUrl { get; set; }
        public Instructor Instructor { get; set; }
        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; }

        public Category Category { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

    }
}
