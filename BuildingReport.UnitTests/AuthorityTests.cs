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
    public class AuthorityTests
    {/*
        private Mock<IAuthorityService> _AuthorityServiceMock;
        private AuthorityController _AuthorityController;

        [SetUp]
        public void Setup()
        {
            _AuthorityServiceMock = new Mock<IAuthorityService>();
            _AuthorityController = new AuthorityController(_AuthorityServiceMock.Object);

        }

        [Test]
        public void CreateAuthority_WithNullAuthority_ReturnsBadRequest()
        {
            //arrange
            AuthorityDTO authorityDTO = null;


            //action
            IActionResult result =  _AuthorityController.CreateAuthority(authorityDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void CreateAuthority_WithExistingAuthority_ReturnsUnprocessableContent()
        {
            //arrange

            AuthorityDTO authorityDTO = new AuthorityDTO()
            {
                Name = "Test",
            };

            _AuthorityServiceMock.Setup(i => i.AuthorityExists("Test")).Returns(true);

            //action
            IActionResult result = _AuthorityController.CreateAuthority(authorityDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, statusCode);
        }

        [Test]
        public void CreateAuthority_WithNewAuthority_ReturnsOk()
        {
            //arrange

            AuthorityDTO authorityDTO = new AuthorityDTO()
            {
                Name = "Test",
            };

            _AuthorityServiceMock.Setup(i => i.AuthorityExists("Test")).Returns(false);

            //action
            IActionResult result = _AuthorityController.CreateAuthority(authorityDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }


        [Test]
        public void GetAuthorities_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _AuthorityController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _AuthorityController.GetAuthorities();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetAuthorities_WithValidModelState_ReturnsOK()
        {
            //arrange


            //action
            IActionResult result = _AuthorityController.GetAuthorities();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetAuthoritiesWithID_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _AuthorityController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _AuthorityController.GetAuthorities();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetAuthoritiesWithID_WithValidModelState_ReturnsOK()
        {
            //arrange
            Authority authority = new Authority()
            {
                Id = 1,
                Name = "Test",

            };

            _AuthorityServiceMock.Setup(i => i.GetAuthorityById(1)).Returns(authority);

            //action
            IActionResult result = _AuthorityController.GetAuthorities();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UpdateAuthority_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            AuthorityDTO authorityDTO = new AuthorityDTO()
            {
                Id = 1,
                Name = "Test",

            };
            _AuthorityController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _AuthorityController.Put(authorityDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void UpdateAuthority_WithValidModelState_ReturnsOK()
        {
            //arrange
            Authority authority = new Authority()
            {
                Id = 1,
                Name = "Test",

            };

            AuthorityDTO authorityDTO = new AuthorityDTO()
            {
                Id = 1,
                Name = "Test",

            };

            _AuthorityServiceMock.Setup(i => i.UpdateAuthority(authority)).Returns(authority);

            //action
            IActionResult result = _AuthorityController.Put(authorityDTO);



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void Delete_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            Authority authority = new Authority()
            {
                Id = 1,
                Name = "Test",

            };
            _AuthorityController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _AuthorityController.Delete(1);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void DeleteAuthority_WithValidModelState_ReturnsOK()
        {
            //arrange
            Authority authority = new Authority()
            {
                Id = 1,
                Name = "Test",

            };

           

            //action
            IActionResult result = _AuthorityController.Delete(authority.Id);



            //assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }*/
    }
}