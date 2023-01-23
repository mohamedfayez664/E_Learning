using E_LearningTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;

        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        [HttpPost("LinkRoleRights")]
        //  [Authorize(Roles = "LinkRoleRights")]
        public IActionResult LinkRoleRights(string _roleName, List<string> _righs)
        {
            var res = _roleServices.LinkRoleRights(_roleName, _righs);
            return Ok(res);
        }


    }
}
