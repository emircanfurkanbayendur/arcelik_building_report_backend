using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAuthorityController : ControllerBase
    {
        private IRoleAuthorityService _RoleAuthorityService;
        


        public RoleAuthorityController(IRoleAuthorityService roleAuthorityService)
        {
            _RoleAuthorityService = roleAuthorityService;
            //_RoleAuthorityService = new RoleAuthorityManager();

            
        }

        [HttpGet]
        public IActionResult GetRoleAuthorities()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_RoleAuthorityService.GetAllRoleAuthorities());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleAuthorities(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_RoleAuthorityService.GetRoleAuthorityById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] RoleAuthorityRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _RoleAuthorityService.CreateRoleAuthority(request);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateRoleAuthorityRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _RoleAuthorityService.UpdateRoleAuthority(request);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _RoleAuthorityService.DeleteRoleAuthority(id);

            if (!response)
                return Unauthorized();

            return NoContent();
        }


    }
}
