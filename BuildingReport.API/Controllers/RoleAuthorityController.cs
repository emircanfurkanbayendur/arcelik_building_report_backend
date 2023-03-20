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
        public IActionResult Post([FromBody] RoleAuthorityDTO roleauthoritydto)
        {
            if (roleauthoritydto == null)
            {
                return BadRequest(ModelState);
            }
            //Role role = _roleService.GetRoleById(roleauthoritydto.RoleId);
            //Authority authority = _authorityService.GetAuthorityById(roleauthoritydto.AuthorityId);
            var roleauthority = new RoleAuthority()
            {
                Id = roleauthoritydto.Id,
                RoleId = roleauthoritydto.RoleId,
                AuthorityId = roleauthoritydto.AuthorityId,

                //Role = role,
                //Authority = authority

            };

            //if (_RoleAuthorityService.RoleAuthorityExists(roleauthority.Role.Name, roleauthority.Authority.Name))
            //{
            //    ModelState.AddModelError("", "RoleAuthority already exists");
            //    return StatusCode(422, ModelState);
            //}

            return Ok(_RoleAuthorityService.CreateRoleAuthority(roleauthority));
        }

        [HttpPut]
        public IActionResult Put([FromBody] RoleAuthorityDTO roleauthoritydto)
        {
            var roleauthority = new RoleAuthority()
            {
                Id = roleauthoritydto.Id,
                RoleId = roleauthoritydto.RoleId,
                AuthorityId = roleauthoritydto.AuthorityId

            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_RoleAuthorityService.UpdateRoleAuthority(roleauthority));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _RoleAuthorityService.DeleteRoleAuthority(id);

            return NoContent();
        }


    }
}
