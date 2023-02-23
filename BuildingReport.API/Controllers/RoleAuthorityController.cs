using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
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

        [HttpGet("/byRoleId/{id}")]
        public RoleAuthority GetRoleAuthoritiesByRoleId(long id)
        {
            return _authorityService.GetRoleAuthorityByRoleId(id);
        }

        [HttpGet("/byAuthorityId/{id}")]
        public RoleAuthority GetRoleAuthoritiesByAuthorityId(long id)
        {
            return _authorityService.GetRoleAuthorityByRoleId(id);
        }

        [HttpPost]
        public RoleAuthority Post([FromBody] RoleAuthority authority)
        {
            return _authorityService.CreateRoleAuthority(authority);
        }

        [HttpPut]
        public RoleAuthority Put([FromBody] RoleAuthority authority)
        {
            return _authorityService.UpdateRoleAuthority(authority);
        }

        [HttpDelete("/byRoleId/{id}")]
        public void DeleteByRoleId(long id)
        {
            _authorityService.DeleteRoleAuthorityByRoleId(id);
        }

        [HttpDelete("/byAuthorityId/{id}")]
        public void DeleteByAuthorityId(long id)
        {
            _authorityService.DeleteRoleAuthorityByAuthorityId(id);
        }
    }
}
