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
    {
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
            Authority authority = null;


            //action
            IActionResult result =  _AuthorityController.CreateAuthority(authority);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void CreateAuthority_WithExistingAuthority_ReturnsUnprocessableContent()
        {
            //arrange

            Authority authority = new Authority()
            {
                Name = "Test",
            };

            _AuthorityServiceMock.Setup(i => i.AuthorityExists("Test")).Returns(true);

            //action
            IActionResult result = _AuthorityController.CreateAuthority(authority);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, statusCode);
        }

        [Test]
        public void CreateAuthority_WithNewAuthority_ReturnsOk()
        {
            //arrange

            Authority authority = new Authority()
            {
                Name = "Test",
            };

            _AuthorityServiceMock.Setup(i => i.AuthorityExists("Test")).Returns(false);

            //action
            IActionResult result = _AuthorityController.CreateAuthority(authority);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

    }
}