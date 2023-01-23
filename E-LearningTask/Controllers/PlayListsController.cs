using DAL.DTO;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListsController : ControllerBase
    {
        private readonly IPlayListServices _playListServices;
        public PlayListsController(IPlayListServices playListServices)
        {
            _playListServices = playListServices;
        }

        [HttpPost("AddPlayList")]
        //[Authorize(Roles = "AddPlayList")]
        public IActionResult AddPlayList([FromForm] PlayListAddDto model)
        {
            var res = _playListServices.AddPlayList(model);
            if (res == false)
            {
                return BadRequest("No PlayList Added");
            }
            return Ok(res);
        }
    }
}
