using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {

        private IJWTAuthenticationService _jWTAuthenticationService;


        public LoginController(IJWTAuthenticationService jWTAuthenticationService)
        {
            _jWTAuthenticationService = jWTAuthenticationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ReturnDto returnDto = _jWTAuthenticationService.Login(loginDto);

            if (returnDto == null)
                return Unauthorized();

            return Ok(returnDto);
        }
    }
}
