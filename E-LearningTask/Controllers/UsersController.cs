using DAL.DTO;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        [HttpPost("Register")]
        public ActionResult Register([FromForm] UserRegisterDto model)
        {

            if (_userServices.IsEmailExist(model.Email))
            {
                // ModelState.AddModelError("11", _localizer["EmaiExist"].Value);
                ModelState.AddModelError("EX", "EmaiExist");
            }
            if (ModelState.IsValid)
            {
                var res = _userServices.Register(model);

                ///
                if (res == false)
                {
                    return BadRequest("User Didn't Register");
                }
                return Ok("Please check your email to activate your account");
            }

            return BadRequest(ModelState);
        }


        [HttpGet("ActiveUser")]
        public ActionResult ActiveUser(int id)
        {
            //active user
            var res = _userServices.ActiveUser(id);

            if (res == false) return BadRequest("No user found");

            return Ok("Successfuly activatied");
        }


        [HttpPost("AddUserInstractor")]
        //  [Authorize(Roles = "AddUserInstractor")]
        public ActionResult AddUserInstractor([FromForm] UserInstructorAddDto model)
        {
            var res = _userServices.AddUserInstractor(model);

            if (res == false)
            {
                return BadRequest("UserInstractor Didn't Register");
            }

            return Ok("Please check your email to activate your account");
        }


        [HttpPut("EditUserInstractor")]
        //  [Authorize(Roles = "EditUserInstractor")]
        public ActionResult EditUserInstractor(int id, [FromForm] UserInstructorEditDto model)
        {
            var res = _userServices.EditUserInstractor(id, model);

            if (res == false)
            {
                return BadRequest("UserInstractor Didn't Edited");
            }

            return Ok("Edit Done");
        }


        [HttpPost("Login")]   /////to avoid quary
        public ActionResult Login(UserLoginDto model)
        {
            var res = _userServices.Login(model);
            if (res == null)
            {
                return BadRequest("User Name Or Password Not Correct");
            }

            return Ok(res);      ////Token
        }

        [HttpGet("GetUserRighits")]
       // [Authorize(Roles = "GetUserRighits")]
        public IActionResult GetUserRighits(int id)
        {
            var res = _userServices.GetUserRighits(id);

            if (res.Count == 0)
            {
                return BadRequest("No output");
            }
            return Ok(res);
        }


        [HttpGet("StartUpTheProject")]
        //  [Authorize(Roles = "StartUpTheProject")]
        public IActionResult StartUpTheProject()
        {
            var res = _userServices.StartUpTheProject();
            if (res == false) { return BadRequest("StartUpTheProject had Issue"); }

            return Ok("Startup Done");
        }


        [HttpPost("LinkUserRole")]
        //  [Authorize(Roles = "LinkUserRole")]
        public IActionResult LinkUserRole(int id, string role)
        {
            var res = _userServices.LinkUserRole(id, role);

            if (res == false) { return BadRequest("No Link"); }

            return Ok("LinkUserRole Done");
        }
    }
}
