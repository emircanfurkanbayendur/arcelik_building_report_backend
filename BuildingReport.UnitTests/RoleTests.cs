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
    public class RoleTests
    {
        private Mock<IRoleService> _roleServiceMock;
        private RoleController _roleController;

        [SetUp]
        public void Setup()
        {
            _roleServiceMock = new Mock<IRoleService>();
            _roleController = new RoleController(_roleServiceMock.Object);

        }

        [Test]
        public void CreateRole_WithNullRole_ReturnsBadRequest()
        {
            //arrange
            RoleDTO roleDTO = null;


            //action
            IActionResult result = _roleController.CreateRole(roleDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void CreateRole_WithExistingRole_ReturnsUnprocessableContent()
        {
            //arrange

            RoleDTO roleDTO = new RoleDTO()
            {
                Name = "Test",
            };

            _roleServiceMock.Setup(i => i.RoleExists("Test")).Returns(true);

            //action
            IActionResult result = _roleController.CreateRole(roleDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, statusCode);
        }

        [Test]
        public void CreateRole_WithNewRole_ReturnsOk()
        {
            //arrange

            RoleDTO roleDTO = new RoleDTO()
            {
                Name = "Test",
            };

            _roleServiceMock.Setup(i => i.RoleExists("Test")).Returns(false);

            //action
            IActionResult result = _roleController.CreateRole(roleDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

    }
}