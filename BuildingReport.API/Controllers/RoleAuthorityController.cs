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
        //private IRoleService _roleService;
        //private IAuthorityService _authorityService;

        public RoleAuthorityController()
        {
            _RoleAuthorityService = new RoleAuthorityManager();
            //_roleService = new RoleManager();
            //_authorityService = new AuthorityManager();
            
        }

        [HttpGet]
        public List<RoleAuthority> GetRoleAuthorities()
        {
            return _RoleAuthorityService.GetAllRoleAuthorities();
        }

        [HttpGet("{id}")]
        public RoleAuthority GetRoleAuthorities(long id)
        {
            return _RoleAuthorityService.GetRoleAuthorityById(id);
        }

        [HttpPost]
        public RoleAuthority Post([FromBody] RoleAuthorityDTO roleauthoritydto)
        {
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


            return _RoleAuthorityService.CreateRoleAuthority(roleauthority);
        }

        [HttpPut]
        public RoleAuthority Put([FromBody] RoleAuthorityDTO roleauthoritydto)
        {
            var roleauthority = new RoleAuthority()
            {
                Id = roleauthoritydto.Id,
                RoleId = roleauthoritydto.RoleId,
                AuthorityId = roleauthoritydto.AuthorityId

            };

            return _RoleAuthorityService.UpdateRoleAuthority(roleauthority);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _RoleAuthorityService.DeleteRoleAuthority(id);
        }


    }
}
