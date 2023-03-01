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
        private IRoleAuthorityService _authorityService;

        public RoleAuthorityController()
        {
            _authorityService = new RoleAuthorityManager();
        }

        [HttpGet]
        public List<RoleAuthority> GetRoleAuthorities()
        {
            return _authorityService.GetAllRoleAuthorities();
        }

        [HttpGet("{id}")]
        public RoleAuthority GetRoleAuthorities(long id)
        {
            return _authorityService.GetRoleAuthorityById(id);
        }

        [HttpPost]
        public RoleAuthority Post([FromBody] RoleAuthorityDTO roleauthoritydto)
        {
            var roleauthority = new RoleAuthority()
            {
                Id = roleauthoritydto.Id,
                RoleId = roleauthoritydto.RoleId,
                AuthorityId = roleauthoritydto.AuthorityId

            };


            return _authorityService.CreateRoleAuthority(roleauthority);
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

            return _authorityService.UpdateRoleAuthority(roleauthority);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _authorityService.DeleteRoleAuthority(id);
        }

    }
}
