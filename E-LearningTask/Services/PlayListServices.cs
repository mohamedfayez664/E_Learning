using AutoMapper;
using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class PlayListServices : IPlayListServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;

        public PlayListServices(IMapper mapper, ApplicationDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public bool AddPlayList(PlayListAddDto model)
        {
            var playList = _mapper.Map<PlayList>(model);
            try
            {
                _context.PlayLists.Add(playList);
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
