using DAL.DTO;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StGroupsController : ControllerBase
    {
        private readonly IStGroupServices _stGroupServices;

        public StGroupsController(IStGroupServices stGroupServices)
        {
            _stGroupServices = stGroupServices;
        }

        [HttpPost("AddStGroup")]
        //[Authorize(Roles = "AddStGroup")]
        public IActionResult AddStGroup([FromForm] StGroupAddDto model)
        {
            var res = _stGroupServices.AddStGroup(model);
            if (res == false)
            {
                return BadRequest("No Student Group Added");
            }
            return Ok(res);
        }

        [HttpGet("GetGroupDetails")]
        //[Authorize(Roles = "GetGroupDetails")]
        public IActionResult GetGroupDetails(int id)
        {
            var res = _stGroupServices.GetGroupDetails(id);

            if (res == null) return BadRequest("No details");

            return Ok(res);
        }
    }
}
