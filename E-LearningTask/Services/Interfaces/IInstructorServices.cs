using DAL.DTO;

namespace E_LearningTask.Services.Interfaces
{
    public interface IInstructorServices
    {
        bool AddInstructor(InstructorAddDto model);
    }
}
