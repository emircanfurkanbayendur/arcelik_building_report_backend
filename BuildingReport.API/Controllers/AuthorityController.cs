using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BuildingReport.DTO.Request;

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
        public IActionResult Create([FromBody] AuthorityRequest request)
        {          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_authorityService.CreateAuthority(request));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateAuthorityRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_authorityService.UpdateAuthority(request));
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
