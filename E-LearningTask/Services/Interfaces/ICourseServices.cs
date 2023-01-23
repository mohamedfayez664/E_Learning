using DAL.DTO;

namespace E_LearningTask.Services.Interfaces
{
    public interface ICourseServices
    {
        bool AddCourse(CourseAddDto model);
        bool EditCourse(int id, CourseEditDto model);
        bool AddRate(int id, UserCourseRateDto model);
        bool AddImageAndFile(int id, CourseAccessFilesDto model);
        List<CourseGetDetailDto> GetAllCoursesDetil();
    }
}
