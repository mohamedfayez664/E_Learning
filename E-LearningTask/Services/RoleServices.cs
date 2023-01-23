using DAL.Data;
using DAL.Entities;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly ApplicationDBContext _context;

        public RoleServices(ApplicationDBContext context)
        {
            _context = context;
        }


        public bool LinkRoleRights(string _roleName, List<string> _righs)
        {
            ///Check.....
            ///
            var _roleId = _context.Roles.FirstOrDefault(r => r.Name == _roleName).Id;
            try
            {
                foreach (var _rigtname in _righs)
                {
                    var _rightsId = _context.Rights.FirstOrDefault(r => r.Name == _rigtname).Id;

                    var _roleright = new RoleRight();
                    _roleright.RoleId = _roleId;
                    _roleright.RightId = _rightsId;

                    if ((_context.RoleRights.FirstOrDefault(r => r.RightId == _rightsId || r.RoleId == _roleId)) == null)
                    {
                        _context.RoleRights.Add(_roleright);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
    }
}
