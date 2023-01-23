using DAL.DTO;

namespace E_LearningTask.Services.Interfaces
{
    public interface ILessonServices
    {
        bool AddLesson(LessonAddDto model);
        bool AddLessonData(int id, MediaAddTypeDto model);
        LessonGetDataDto GetLessonDetails(int id);
        bool EditLessonData(int id, MediaEditDataDto model);
    }
}
