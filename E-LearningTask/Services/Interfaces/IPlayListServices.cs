using DAL.DTO;

namespace E_LearningTask.Services.Interfaces
{
    public interface IPlayListServices
    {
        bool AddPlayList(PlayListAddDto model);
    }
}
