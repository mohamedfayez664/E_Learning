using DAL.Data;
using DAL.Entities;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class UserGroupServices : IUserGroupServices
    {
        private readonly ApplicationDBContext _context;
        public UserGroupServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool LinkUserToGroup(int user_id, int group_id)
        {
            /////
            var _usergroup = _context.UserGroups.Any(uc => (uc.StGroupId == group_id) && (uc.UserId == user_id));
            if (_usergroup == true) return false;
            if (_context.Users.Find(user_id) != null && _context.StGroups.Find(group_id) != null)
            {
                var _usergroupLink = new UserGroup
                {
                    StGroupId = group_id,
                    UserId = user_id,
                };
                _context.UserGroups.Add(_usergroupLink);
                _context.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
