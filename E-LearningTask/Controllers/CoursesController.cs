using DAL.DTO;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private readonly ICourseServices _courseServices;

        public CoursesController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        // GET: api/<CoursesController>
        [HttpGet("GetAllCoursesDetil")]
        public IActionResult GetAllCoursesDetil()
        {
            var res = _courseServices.GetAllCoursesDetil();
            if (res == null)
            {
                return BadRequest("No Course Found");

            }
            return Ok(res);
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CoursesController>
        [HttpPost("AddCourse")]
        //  [Authorize(Roles = "AddCourse")]
        public IActionResult AddCourse([FromBody] CourseAddDto model)
        {
            var res = _courseServices.AddCourse(model);
            if (res == false)
            {
                return BadRequest("No Course Added");
            }
            return Ok(res);
        }
        // POST api/<CoursesController>
        [HttpPost("AddImageAndFile{id}")]
        //  [Authorize(Roles = "AddImageAndFile")]
        public IActionResult AddImageAndFile(int id, [FromForm] CourseAccessFilesDto model)
        {
            var res = _courseServices.AddImageAndFile(id, model);
            if (res == false)
            {
                return BadRequest("No Imqage Added ");
            }
            return Ok(res);
        }


        // PUT api/<CoursesController>/5
        [HttpPut("EditCourse{id}")]
        //  [Authorize(Roles = "EditCourse")]
        public IActionResult EditCourse(int id, [FromForm] CourseEditDto model)
        {
            var res = _courseServices.EditCourse(id, model);
            if (res == false)
            {
                return BadRequest("No Data Updated");
            }
            return Ok("Update Done");
        }


        [HttpPut("AddRate{id}")]
        //  [Authorize(Roles = "AddRate")]
        public IActionResult AddRate(int id, [FromForm] UserCourseRateDto model)
        {
            var res = _courseServices.AddRate(id, model);
            if (res == false)
            {
                return BadRequest("No rate Added");
            }
            return Ok("Rate Done");
        }


        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
