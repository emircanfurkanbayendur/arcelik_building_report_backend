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

        public AuthorityController(IAuthorityService authorityService)
        {
            _authorityService = authorityService;
            //_authorityService = new AuthorityManager();
        }

        [HttpGet]
        public IActionResult GetAuthorities()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_authorityService.GetAllAuthorities());
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorities(long id) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_authorityService.GetAuthorityById(id));
        }

        [HttpPost]
        public IActionResult CreateAuthority([FromBody] AuthorityDTO authorityDTO)
        {
            
            if (authorityDTO == null)
            {
                return BadRequest(ModelState);
            }

            Authority authority = new Authority()
            {
                Id = authorityDTO.Id,
                Name = authorityDTO.Name,

            };

            if (_authorityService.AuthorityExists(authority.Name))
            {
                ModelState.AddModelError("", "Authority already exists");
                return StatusCode(422, ModelState);
            }

            _authorityService.CreateAuthority(authority);

            return Ok(authority);
        }

        [HttpPut]
        public IActionResult Put([FromBody] AuthorityDTO authorityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Authority authority = new Authority()
            {
                Id = authorityDTO.Id,
                Name = authorityDTO.Name,

            };

            return Ok(_authorityService.UpdateAuthority(authority));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _authorityService.DeleteAuthority(id);

            return NoContent();
        }

    }

}
