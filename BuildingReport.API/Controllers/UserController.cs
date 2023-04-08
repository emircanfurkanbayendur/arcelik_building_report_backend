using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
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
        public IActionResult CreateUser([FromBody] UserDTO userdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            return Ok(_userService.CreateUser(userdto));
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
