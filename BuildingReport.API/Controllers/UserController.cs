using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
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
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string mail)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.ForgotPassword(mail);

             return Ok ();
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDTO userdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_userService.UpdateUser(userdto));
        }

        [HttpPut("changeRole/{userId}")]
        public IActionResult UpdateUserRole(long userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_userService.UpdateUserRole(userId));
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.DeleteUser(id);

            return NoContent();
        }
    }
}
