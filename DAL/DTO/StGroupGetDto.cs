namespace DAL.DTO
{
    public class StGroupGetDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberofUsers { get; set; }  //Calculating properity
        public InstructorGetDto Instructor { get; set; }
        public List<UserGetDto> Students { get; set; }

    }
}
