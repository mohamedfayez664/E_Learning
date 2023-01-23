namespace E_LearningTask.Services.Interfaces
{
    public interface IRoleServices
    {
        bool LinkRoleRights(string _roleName, List<string> _righs);
    }
}
