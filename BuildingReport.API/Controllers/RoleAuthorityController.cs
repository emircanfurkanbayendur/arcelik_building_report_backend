using AutoMapper;
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
        private readonly IMapper _mapper;


        public RoleAuthorityController(IRoleAuthorityService roleAuthorityService, IMapper mapper)
        {
            _RoleAuthorityService = roleAuthorityService;
            _mapper = mapper;
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
 
           RoleAuthority roleAuthority = _mapper.Map<RoleAuthority>(roleauthoritydto);

            //var roleauthority = new RoleAuthority()
            //{
            //    Id = roleauthoritydto.Id,
            //    RoleId = roleauthoritydto.RoleId,
            //    AuthorityId = roleauthoritydto.AuthorityId,



            //};


            //if (_RoleAuthorityService.RoleAuthorityExists(roleauthority.Role.Name, roleauthority.Authority.Name))
            //{
            //    ModelState.AddModelError("", "RoleAuthority already exists");
            //    return StatusCode(422, ModelState);
            //}

            return Ok(_RoleAuthorityService.CreateRoleAuthority(roleAuthority));
        }

        [HttpPut]
        public IActionResult Put([FromBody] RoleAuthorityDTO roleauthoritydto)
        {
            var roleAuthority = _mapper.Map<RoleAuthority>(roleauthoritydto);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_RoleAuthorityService.UpdateRoleAuthority(roleAuthority));
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
