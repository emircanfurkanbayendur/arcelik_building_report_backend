using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;


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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_roleService.CreateRole(roleDTO));
        }

        [HttpPut]
        public IActionResult Put([FromBody] RoleDTO roleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_roleService.UpdateRole(roleDTO));
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
