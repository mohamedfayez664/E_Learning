using DAL.DTO;

namespace E_LearningTask.Services.Interfaces
{
    public interface ICategoryServices
    {
        bool AddCategory(CategoryAddDto model);
    }
}
