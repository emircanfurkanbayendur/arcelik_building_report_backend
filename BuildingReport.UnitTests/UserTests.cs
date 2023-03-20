using Moq;
using NUnit.Framework;
using BuildingReport.API.Controllers;
using BuildingReport.DTO;
using BuildingReport.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;

namespace BuildingReport.UnitTests

{
    public class UserTests
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<IRoleService> _roleServiceMock;
        private Mock<IJWTAuthenticationService> _jwtAuthenticationServiceMock;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _roleServiceMock = new Mock<IRoleService>();
            _jwtAuthenticationServiceMock = new Mock<IJWTAuthenticationService>();
            _userController = new UserController(_jwtAuthenticationServiceMock.Object,_userServiceMock.Object,_roleServiceMock.Object);

        }

        [Test]
        public void CreateUser_WithNullUser_ReturnsBadRequest()
        {
            //arrange
            UserDTO UserDTO = null;

            


            //action
            IActionResult result = _userController.CreateUser(UserDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void CreateUser_WithExistingUser_ReturnsUnprocessableContent()
        {
            //arrange

            UserDTO UserDTO = new UserDTO()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "TestMail",
                Password = "password"
                
            };

            _userServiceMock.Setup(i => i.UserExists("TestMail")).Returns(true);

            //action
            IActionResult result = _userController.CreateUser(UserDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, statusCode);
        }

        [Test]
        public void CreateUser_WithNewUser_ReturnsOk()
        {
            //arrange

            UserDTO UserDTO = new UserDTO()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "TestMail",
                Password = "password"

            };

            Role role = new Role()
            {
                Id = 1,
                Name = "guest"
            };

            List<Role> roles = new List<Role>() { role };


            _roleServiceMock.Setup(i => i.GetAllRoles()).Returns(roles);
            _userServiceMock.Setup(i => i.UserExists("TestMail")).Returns(false);

            //action
            IActionResult result = _userController.CreateUser(UserDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }


        [Test]
        public void Authenticate_WithNullToken_ReturnsUnauthorized()
        {
            //arrange

            LoginDto loginDto = new LoginDto()
            {
                Email = "test@mail.com",
                Password = "wrongpassword"
            };

            var _password = Hash.HashPassword(loginDto.Password);

             string token = null;
            _jwtAuthenticationServiceMock.Setup(i => i.Authenticate(loginDto.Email, _password)).Returns(token);

            //action
            IActionResult result = _userController.Authenticate(loginDto);


            //assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void Authenticate_WithToken_ReturnsOK()
        {
            //arrange

            LoginDto loginDto = new LoginDto()
            {
                Email = "test@mail.com",
                Password = "password"
            };

            var _password = Hash.HashPassword(loginDto.Password);

            User user = new User()
            {
                Email = "test@mail.com",
                Password = _password
            };

            List<User> users = new List<User>() { user };

            _userServiceMock.Setup(i => i.GetAllUsers()).Returns(users); 



            string token = "a_token";
            _jwtAuthenticationServiceMock.Setup(i => i.Authenticate(loginDto.Email, _password)).Returns(token);

            //action
            IActionResult result = _userController.Authenticate(loginDto);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);

        }

    }
}