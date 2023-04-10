using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BuildingReport.DTO.Request;
using Azure;

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
        public IActionResult CreateRole([FromBody] RoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _roleService.CreateRole(request);

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateRoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _roleService.UpdateRole(request);

            return Ok(response);
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
