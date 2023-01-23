using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupServices _userGroupServices;

        public UserGroupController(IUserGroupServices userGroupServices)
        {
            _userGroupServices = userGroupServices;
        }

        [HttpPost("LinkUserToGroup")]
        //  [Authorize(Roles = "LinkUserToGroup")]
        public IActionResult LinkUserToGroup(int user_id, int group_id)
        {
            var res = _userGroupServices.LinkUserToGroup(user_id, group_id);
            if (res == false)
            {
                return BadRequest("No Link Added");
            }
            return Ok("LinkUserToGroup Done");
        }
    }
}
