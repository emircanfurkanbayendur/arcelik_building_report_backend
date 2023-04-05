using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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
        }

        [HttpGet]
        public IActionResult GetAuthorities()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_authorityService.GetAllAuthorities());
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthority(long id) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_authorityService.GetAuthorityById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] AuthorityDTO authorityDTO)
        {          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_authorityService.CreateAuthority(authorityDTO));
        }

        [HttpPut]
        public IActionResult Update([FromBody] AuthorityDTO authorityDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_authorityService.UpdateAuthority(authorityDTO));
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
