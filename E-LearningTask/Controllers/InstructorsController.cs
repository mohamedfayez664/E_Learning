using DAL.DTO;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorServices _instructorServices;

        public InstructorsController(IInstructorServices instructorServices)
        {
            _instructorServices = instructorServices;
        }

        [HttpPost("AddInstructor")]
        //  [Authorize(Roles = "AddInstructor")]
        public IActionResult AddInstructor([FromForm] InstructorAddDto model)
        {
            var res = _instructorServices.AddInstructor(model);
            if (res == false)
            {
                return BadRequest("No Instructor Added");
            }
            return Ok(res);
        }
    }
}
