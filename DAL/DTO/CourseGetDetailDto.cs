namespace DAL.DTO
{
    public class CourseGetDetailDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public InstructorGetDto Instructor { get; set; }

        public CategoryGetDto Category { get; set; }
        public List<PlayListGetDto> PlayLists { get; set; }
    }
}
