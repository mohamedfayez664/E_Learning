using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCourseController : ControllerBase
    {
        private readonly IUserCourseServices _userCourseServices;

        public UserCourseController(IUserCourseServices userCourseServices)
        {
            _userCourseServices = userCourseServices;
        }

        [HttpPost("LinkUserToCourse")]
        //  [Authorize(Roles = "LinkUserToCourse")]
        public IActionResult LinkUserToCourse(int user_id, int course_id)
        {
            var res = _userCourseServices.LinkUserToCourse(user_id, course_id);
            if (res == false)
            {
                return BadRequest("No Link Added");
            }
            return Ok("LinkUserToCourse Done");
        }
    }
}
