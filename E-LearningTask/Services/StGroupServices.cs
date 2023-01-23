using AutoMapper;
using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class StGroupServices : IStGroupServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;

        public StGroupServices(IMapper mapper, ApplicationDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public StGroupGetDto GetGroupDetails(int id)
        {

            var group = _context.StGroups.Find(id);
            if (group == null) return null;

            var _stGroupGetDto = _mapper.Map<StGroupGetDto>(group);

            var _students = _context.UserGroups.Where(ug => ug.StGroupId == group.Id)
                            .Join(_context.Users, ug => ug.UserId, u => u.Id, (ug, u)
                            => u).ToList();
            var _instructor = _students.Join(_context.Instructors, s => s.Id, i => i.UserId, (s, i)
                           => i).FirstOrDefault();
            var list = new List<UserGetDto>();

            foreach (var item in _students)
            {
                var _userGetDto = _mapper.Map<UserGetDto>(item);
                list.Add(_userGetDto);
            }

            _stGroupGetDto.Students = list;
            _stGroupGetDto.Instructor = _mapper.Map<InstructorGetDto>(_instructor);

            return _stGroupGetDto;
        }



        public bool AddStGroup(StGroupAddDto model)
        {
            var stGroup = _mapper.Map<StGroup>(model);
            try
            {
                _context.StGroups.Add(stGroup);
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
