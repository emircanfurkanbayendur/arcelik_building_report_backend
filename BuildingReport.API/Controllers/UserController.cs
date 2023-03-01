using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace BuildingReport.API.Controllers
{
    public class Hash
    {
        public static byte[]  HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            return hashBytes;

        }
    }
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        public UserController(IJWTAuthenticationService jwtAuthenticationService)
        {
            _userService = new UserManager();
            _jwtAuthenticationService = jwtAuthenticationService;   
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromQuery] string email, [FromQuery] string password)
        {
            var token = _jwtAuthenticationService.Authenticate(email, password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return _userService.GetAllUsers();
        }


        [HttpGet("{id}")]
        public User GetUsers(long id)
        {
            return _userService.GetUserById(id);

        }

        [HttpGet("role/{roleid}")]
        public List<User> GetUsersByRoleID(long roleid)
        {
            return _userService.GetUsersByRole(roleid);

        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO userdto)
        {
            if (userdto == null)
            {
                return BadRequest(ModelState);
            }
            var user = new User()
            {
                Id = userdto.Id,
                FirstName = userdto.FirstName,
                LastName = userdto.LastName,
                Email = userdto.Email,
                Password = Hash.HashPassword(userdto.Password),
                CreatedAt = userdto.CreatedAt,
                IsActive = userdto.IsActive,
                RoleId = userdto.RoleId
            };


            if (_userService.UserExists(user.Email))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            _userService.CreateUser(user);

            return Ok("Successfuly ctreated");
        }

        [HttpPut]
        public User Put([FromBody] UserDTO userdto)
        {
            var user = new User()
            {
                Id = userdto.Id,
                FirstName = userdto.FirstName,
                LastName = userdto.LastName,
                Email = userdto.Email,
                Password = Hash.HashPassword(userdto.Password),
                CreatedAt = userdto.CreatedAt,
                IsActive = userdto.IsActive,
                RoleId = userdto.RoleId

            };
            return _userService.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _userService.DeleteUser(id);
        }
    }
}
