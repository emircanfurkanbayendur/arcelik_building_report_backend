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
        public Role Put([FromBody] RoleDTO roleDTO)
        {
            Role role = new Role()
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name,
            };


            return _roleService.UpdateRole(role);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _roleService.DeleteRole(id);
        }

    }
}
