using DAL.DTO;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonServices _lessonServices;
        public LessonsController(ILessonServices lessonServices)
        {
            _lessonServices = lessonServices;
        }

        [HttpPost("AddLesson")]
        //[Authorize(Roles = "AddLesson")]
        public IActionResult AddLesson([FromForm] LessonAddDto model)
        {
            var res = _lessonServices.AddLesson(model);
            if (res == false)
            {
                return BadRequest("No Lesson Added");
            }
            return Ok(res);
        }

        [HttpPost("AddLessonData")]
        //[Authorize(Roles = "AddLessonData")]
        public IActionResult AddLessonData(int id, [FromForm] MediaAddTypeDto model)
        {
            var res = _lessonServices.AddLessonData(id, model);
            if (res == false)
            {
                return BadRequest("No Lesson Data Added");
            }
            return Ok(res);
        }

        [HttpGet("GetLessonDetails")]
        //[Authorize(Roles = "GetLessonDetails")]
        public IActionResult GetLessonDetails(int id)
        {
            var res = _lessonServices.GetLessonDetails(id);

            if (res == null) return BadRequest("No Lesson details");

            return Ok(res);
        }



        [HttpPut("EditLessonData{id}")]
        //  [Authorize(Roles = "EditLessonData")]
        public IActionResult EditLessonData(int id, [FromForm] MediaEditDataDto model)
        {
            var res = _lessonServices.EditLessonData(id, model);
            if (res == false)
            {
                return BadRequest("No Data Edited");
            }
            return Ok("Update Done");
        }

    }
}
