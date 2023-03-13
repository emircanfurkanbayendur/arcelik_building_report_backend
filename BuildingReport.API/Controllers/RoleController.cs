using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;

        public RoleController()
        {
            _roleService = new RoleManager();
        }


        [HttpGet]
        public List<Role> GetRoles()
        {
            return _roleService.GetAllRoles();
        }

        [HttpGet("{id}")]
        public Role GetRoles(long id)
        {
            return _roleService.GetRoleById(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateRole([FromBody] Role Role)
        {
            if (Role == null)
            {
                return BadRequest(ModelState);
            }

            if (_roleService.RoleExists(Role.Name))
            {
                ModelState.AddModelError("", "Role already exists");
                return StatusCode(422, ModelState);
            }

            _roleService.CreateRole(Role);

            return Ok(Role);
        }

        [HttpPut]
        public Role Put([FromBody] Role Role)
        {
            return _roleService.UpdateRole(Role);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _roleService.DeleteRole(id);
        }

    }
}
