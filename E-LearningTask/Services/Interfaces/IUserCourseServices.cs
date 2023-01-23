namespace E_LearningTask.Services.Interfaces
{
    public interface IUserCourseServices
    {
        bool LinkUserToCourse(int user_id, int course_id);
    }
}
