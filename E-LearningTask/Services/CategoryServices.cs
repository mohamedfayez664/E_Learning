using AutoMapper;
using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;

        public CategoryServices(IMapper mapper, ApplicationDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public bool AddCategory(CategoryAddDto model)
        {
            var category = _mapper.Map<Category>(model);
            try
            {
                _context.Categories.Add(category);
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
