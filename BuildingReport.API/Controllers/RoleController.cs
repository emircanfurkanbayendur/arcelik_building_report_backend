using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
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

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
            //_roleService = new RoleManager();
        }


        [HttpGet]
        public IActionResult GetRoles()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_roleService.GetAllRoles());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoles(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_roleService.GetRoleById(id));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateRole([FromBody] RoleDTO roleDTO)
        {

            if (roleDTO == null)
            {
                return BadRequest(ModelState);
            }
            Role role = new Role()
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name,
            };

            if (_roleService.RoleExists(role.Name))
            {
                ModelState.AddModelError("", "Role already exists");
                return StatusCode(422, ModelState);
            }

            _roleService.CreateRole(role);

            return Ok(role);
        }

        [HttpPut]
        public IActionResult Put([FromBody] RoleDTO roleDTO)
        {
            Role role = new Role()
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name,
            };


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_roleService.UpdateRole(role));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _roleService.DeleteRole(id);

            return NoContent();
        }

    }
}
