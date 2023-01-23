using DAL.DTO;
using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpPost("AddCategory")]
        //[Authorize(Roles = "AddCategory")]
        public IActionResult AddCategory([FromForm] CategoryAddDto model)
        {
            var res = _categoryServices.AddCategory(model);
            if (res == false)
            {
                return BadRequest("No Category Added");
            }
            return Ok(res);
        }



    }
}
