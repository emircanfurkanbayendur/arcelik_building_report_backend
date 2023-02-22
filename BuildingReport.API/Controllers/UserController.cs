using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
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
        public User GetUsers(int id)
        {
            return _userService.GetUserById(id);

        }

        [HttpPost]
        public User Post([FromBody] User user)
        {
            return _userService.CreateUser(user);
        }

        [HttpPut]
        public User Put([FromBody] User user)
        {
            return _userService.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _userService.DeleteUser(id);
        }
    }
}
