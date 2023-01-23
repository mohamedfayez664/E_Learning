using DAL.DTO;

namespace E_LearningTask.Services.Interfaces
{
    public interface IPlayListGroupServices
    {
        bool LinkPlayListToGroup(int playList_id, int group_id);
        bool AddDiscussion(PlayListGroupAddDiscussionDto model);
    }
}
