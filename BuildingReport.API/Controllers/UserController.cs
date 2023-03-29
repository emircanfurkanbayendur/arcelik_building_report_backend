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
        private Hash hash = new Hash();
        private IUserService _userService;
        private IRoleService _roleService;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;


        public UserController(IJWTAuthenticationService jwtAuthenticationService, IUserService userService, IRoleService roleService)
        {
            //_userService = new UserManager();
            _userService = userService;
            //_roleService = new RoleManager();
            _roleService = roleService;
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            byte[] _password = hash.HashPassword(loginDto.Password);
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
            if (userdto == null)
            {
                return BadRequest(ModelState);
            }

            if (_userService.UserExists(userdto.Email))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            var roleID = _roleService.GetAllRoles().Where(r => r.Name == "guest").FirstOrDefault().Id;
            var user = new User()
            {
                Id = userdto.Id,
                FirstName = userdto.FirstName,
                LastName = userdto.LastName,
                Email = userdto.Email,
                Password = hash.HashPassword(userdto.Password),
                CreatedAt = userdto.CreatedAt,
                IsActive = userdto.IsActive,
                RoleId = roleID,
            };




            _userService.CreateUser(user);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDTO userdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userService.GetUserById(userdto.Id);
            var new_user = new User()
            {
                Id = userdto.Id,
                FirstName = userdto.FirstName,
                LastName = userdto.LastName,
                Email = userdto.Email,
                Password = hash.HashPassword(userdto.Password),
                CreatedAt = user.CreatedAt,
                IsActive = userdto.IsActive,
                RoleId = user.RoleId
            };


            return Ok(_userService.UpdateUser(new_user));
        }

        [HttpPut("changeRole/{userId}")]
        public IActionResult UpdateUserRole(long userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

            return Ok(_userService.UpdateUser(new_user));
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
