using DAL.Enum;

namespace DAL.DTO
{
    public class UserCourseRateDto
    {
        public string? ReviewText { get; set; }
        public Stars SEvaluation { get; set; }
        public int UserId { get; set; }
        //   public int CourseId { get; set; }
    }
}
