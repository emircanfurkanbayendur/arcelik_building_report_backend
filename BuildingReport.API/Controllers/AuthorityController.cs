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
        public IActionResult CreateAuthority([FromBody] Authority authority)
        {
            if (authority == null)
            {
                return BadRequest(ModelState);
            }

            if (_authorityService.AuthorityExists(authority.Name))
            {
                ModelState.AddModelError("", "Authority already exists");
                return StatusCode(422, ModelState);
            }

            _authorityService.CreateAuthority(authority);

            return Ok("Successfuly ctreated");
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
