using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
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

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController()
        {
            _userService = new UserManager();

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



        [HttpPost]
        public User Post([FromBody] UserDTO userdto)
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

            return _userService.CreateUser(user);
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
