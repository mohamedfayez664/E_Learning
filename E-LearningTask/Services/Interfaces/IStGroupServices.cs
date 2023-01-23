using DAL.DTO;

namespace E_LearningTask.Services.Interfaces
{
    public interface IStGroupServices
    {
        bool AddStGroup(StGroupAddDto model);
        StGroupGetDto GetGroupDetails(int id);
    }
}
