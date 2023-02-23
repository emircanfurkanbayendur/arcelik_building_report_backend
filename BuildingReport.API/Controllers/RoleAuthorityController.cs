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

        [HttpGet("{id}")]
        public RoleAuthority GetRoleAuthorities(long id)
        {
            return _authorityService.GetRoleAuthorityById(id);
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

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _authorityService.DeleteRoleAuthority(id);
        }

    }
}
