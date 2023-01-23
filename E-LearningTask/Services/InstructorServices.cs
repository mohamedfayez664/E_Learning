using AutoMapper;
using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class InstructorServices : IInstructorServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;
        public InstructorServices(IMapper mapper, ApplicationDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public bool AddInstructor(InstructorAddDto model)
        {
            var instructor = _mapper.Map<Instructor>(model);
            try
            {
                _context.Instructors.Add(instructor);
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
