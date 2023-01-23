namespace E_LearningTask.Services.Interfaces
{
    public interface IUserGroupServices
    {
        bool LinkUserToGroup(int user_id, int group_id);
    }
}
