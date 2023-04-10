using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginRequest loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            LoginResponse response = _userService.Login(loginDto);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_userService.GetAllUsers());
        }


        [HttpGet("{id}")]
        public IActionResult GetUsers(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_userService.GetUserById(id));

        }

        [HttpGet("role/{roleid}")]
        public IActionResult GetUsersByRoleID(long roleid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_userService.GetUsersByRole(roleid));

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            return Ok(_userService.CreateUser(request));
        }

        [AllowAnonymous]
        [HttpPost("verifyToken")]
        public IActionResult VerifyToken(string token)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_userService.VerifyToken(token))
                return Ok();

            else return BadRequest();
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest userdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _userService.UpdateUser(userdto);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPut("changeRole/{userId}")]
        public IActionResult UpdateUserRole(long userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _userService.UpdateUserRole(userId);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _userService.DeleteUser(id);

            if (!response)
                return Unauthorized();

            return NoContent();
        }
    }
}
