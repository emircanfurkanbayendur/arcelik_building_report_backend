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
        private IRoleService _roleService;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        public UserController(IJWTAuthenticationService jwtAuthenticationService)
        {
            _userService = new UserManager();
            _roleService = new RoleManager();
            _jwtAuthenticationService = jwtAuthenticationService;   
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            byte[] _password = Hash.HashPassword(loginDto.Password);
            var token = _jwtAuthenticationService.Authenticate(loginDto.Email, _password);
            if (token == null)
            {
                return Unauthorized();
            }

            
            var user = _userService.GetAllUsers().Where(u => u.Email == loginDto.Email && u.Password.SequenceEqual(_password)).FirstOrDefault();
            ReturnDto returnDto = new ReturnDto()
            {
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                FirstName = user.FirstName,
                Id = user.Id,
                IsActive = user.IsActive ,
                LastName = user.LastName ,
                Password = user.Password,
                Token = token,
                RoleId = user.RoleId
                
            };
           
            //User returnuser = user;
            //List<User> emptylist = new List<User>();
            //List<Document> emptydoc = new List<Document>();
            //List<Building>  emptybuilding = new List<Building>();
            //returnuser.Documents = emptydoc;
            //returnuser.Role.Users = emptylist;
            //returnuser.Buildings = emptybuilding;

            return Ok(returnDto);
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO userdto)
        {
            if (userdto == null)
            {
                return BadRequest(ModelState);
            }

            var roleID = _roleService.GetAllRoles().Where(r => r.Name == "guest").FirstOrDefault().Id;
            var user = new User()
            {
                Id = userdto.Id,
                FirstName = userdto.FirstName,
                LastName = userdto.LastName,
                Email = userdto.Email,
                Password = Hash.HashPassword(userdto.Password),
                CreatedAt = userdto.CreatedAt,
                IsActive = userdto.IsActive,
                RoleId = roleID,
            };


            if (_userService.UserExists(user.Email))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            _userService.CreateUser(user);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut]
        public User UpdateUser([FromBody] UserDTO userdto)
        {
            var user = _userService.GetUserById(userdto.Id);
            var new_user = new User()
            {
                Id = userdto.Id,
                FirstName = userdto.FirstName,
                LastName = userdto.LastName,
                Email = userdto.Email,
                Password = Hash.HashPassword(userdto.Password),
                CreatedAt = user.CreatedAt,
                IsActive = userdto.IsActive,
                RoleId = user.RoleId
            };
            return _userService.UpdateUser(new_user);
        }

        [HttpPut("changeRole/{userId}")]
        public User UpdateUserRole(long userId)
        {
            var roleID = _roleService.GetAllRoles().Where(r => r.Name == "admin").FirstOrDefault().Id;
            var user = _userService.GetUserById(userId);
            var new_user = new User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive,
                RoleId = roleID
            };
            return _userService.UpdateUser(new_user);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _userService.DeleteUser(id);
        }
    }
}
