namespace DAL.DTO
{
    public class CourseAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        //public Instructor Instructor { get; set; }
        //[ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        //public Category Category { get; set; }
        //[ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
