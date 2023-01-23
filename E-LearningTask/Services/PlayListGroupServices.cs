using DAL.Data;
using DAL.DTO;
using DAL.Entities;
using E_LearningTask.Services.Interfaces;

namespace E_LearningTask.Services
{
    public class PlayListGroupServices : IPlayListGroupServices
    {
        private readonly ApplicationDBContext _context;
        public PlayListGroupServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public bool LinkPlayListToGroup(int playList_id, int group_id)
        {
            /////
            var _playListgroup = _context.PlayListGroups.Any(pg => (pg.StGroupId == group_id) && (pg.PlayListId == playList_id));
            if (_playListgroup == true) return false;
            if (_context.PlayLists.Find(playList_id) != null && _context.StGroups.Find(group_id) != null)
            {
                var _playListgroupLink = new PlayListGroup
                {
                    StGroupId = group_id,
                    PlayListId = playList_id,
                };
                _context.PlayListGroups.Add(_playListgroupLink);
                _context.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool AddDiscussion(PlayListGroupAddDiscussionDto model)
        {
            var _playListGroups = _context.PlayListGroups.Where(pg => (pg.PlayListId == model.PlayList_id && pg.StGroupId == model.Group_id)).FirstOrDefault();
            _playListGroups.Discussion = _playListGroups.Discussion + "\n NewDisscussion: \n" + model.Discussion;
            try
            {
                _context.PlayListGroups.Update(_playListGroups);
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
