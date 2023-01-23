using DAL.DTO;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListGroupsController : ControllerBase
    {
        private readonly IPlayListGroupServices _playListGroupServices;

        public PlayListGroupsController(IPlayListGroupServices playListGroupServices)
        {
            _playListGroupServices = playListGroupServices;
        }


        [HttpPost("LinkPlayListToGroup")]
        // [Authorize(Roles = "LinkPlayListToGroup")]
        public IActionResult LinkPlayListToGroup(int playList_id, int group_id)
        {
            var res = _playListGroupServices.LinkPlayListToGroup(playList_id, group_id);
            if (res == false)
            {
                return BadRequest("No Link Added");
            }
            return Ok("LinkPlayListToGroup Done");
        }

        [HttpPost("AddDiscussion")]
        // [Authorize(Roles = "AddDiscussion")]
        public IActionResult AddDiscussion([FromForm] PlayListGroupAddDiscussionDto model)
        {
            var res = _playListGroupServices.AddDiscussion(model);
            if (res == false)
            {
                return BadRequest("No Discussion Added");
            }
            return Ok("LinkPlayListToGroup Done");
        }
    }
}
