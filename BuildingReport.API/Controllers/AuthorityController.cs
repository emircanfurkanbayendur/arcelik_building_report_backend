using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private IAuthorityService _authorityService;

        public AuthorityController()
        {
            _authorityService = new AuthorityManager();
        }

        [HttpGet]
        public List<Authority> GetAuthorities()
        {
            return _authorityService.GetAllAuthorities();
        }

        [HttpGet("{id}")]
        public Authority GetAuthorities(long id) 
        {
            return _authorityService.GetAuthorityById(id);
        }

        [HttpPost]
        public Authority Post([FromBody] Authority authority)
        {
            return _authorityService.CreateAuthority(authority);
        }

        [HttpPut]
        public Authority Put([FromBody] Authority authority)
        {
            return _authorityService.UpdateAuthority(authority);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _authorityService.DeleteAuthority(id);
        }

    }

}
