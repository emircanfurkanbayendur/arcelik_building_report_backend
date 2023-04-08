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
    {/*
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

        [Test]
        public void GetRoles_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _roleController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _roleController.GetRoles();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetRoles_WithValidModelState_ReturnsOK()
        {
            //arrange


            //action
            IActionResult result = _roleController.GetRoles();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetRolesWithID_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _roleController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _roleController.GetRoles();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetRolesWithID_WithValidModelState_ReturnsOK()
        {
            //arrange
            Role role = new Role()
            {
                Id = 1,
                Name = "Test",

            };

            _roleServiceMock.Setup(i => i.GetRoleById(1)).Returns(role);

            //action
            IActionResult result = _roleController.GetRoles();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UpdateRole_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            RoleDTO roleDTO = new RoleDTO()
            {
                Id = 1,
                Name = "Test",

            };
            _roleController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _roleController.Put(roleDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void UpdateRole_WithValidModelState_ReturnsOK()
        {
            //arrange
            RoleDTO roleDTO = new RoleDTO()
            {
                Id = 1,
                Name = "Test",

            };

            Role role = new Role()
            {
                Id = 1,
                Name = "Test",

            };


            _roleServiceMock.Setup(i => i.UpdateRole(role)).Returns(role);

            //action
            IActionResult result = _roleController.Put(roleDTO);



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void Delete_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            Role role = new Role()
            {
                Id = 1,
                Name = "Test",

            };
            _roleController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _roleController.Delete(1);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void DeleteRole_WithValidModelState_ReturnsOK()
        {
            //arrange
            Role role = new Role()
            {
                Id = 1,
                Name = "Test",

            };



            //action
            IActionResult result = _roleController.Delete(role.Id);



            //assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }
        */
    }
}