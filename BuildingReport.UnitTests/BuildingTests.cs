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
    public class BuildingTests
    {
        private Mock<IBuildingService> _buildingServiceMock;
        private BuildingController _buildingController;

        [SetUp]
        public void Setup()
        {
            _buildingServiceMock = new Mock<IBuildingService>();
            _buildingController = new BuildingController(_buildingServiceMock.Object);

        }

        [Test]
        public void CreateBuilding_WithNullBuilding_ReturnsBadRequest()
        {
            //arrange
            BuildingDTO BuildingDTO = null;


            //action
            IActionResult result = _buildingController.CreateBuilding(BuildingDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void CreateBuilding_WithExistingBuilding_ReturnsUnprocessableContent()
        {
            //arrange

            BuildingDTO BuildingDTO = new BuildingDTO()
            {
                Code = "123",
            };

            _buildingServiceMock.Setup(i => i.BuildingExists("123")).Returns(true);

            //action
            IActionResult result = _buildingController.CreateBuilding(BuildingDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, statusCode);
        }

        [Test]
        public void CreateBuilding_WithNewBuilding_ReturnsOk()
        {
            //arrange

            BuildingDTO BuildingDTO = new BuildingDTO()
            {
                Name = "Test",
            };

            _buildingServiceMock.Setup(i => i.BuildingExists("Test")).Returns(false);

            //action
            IActionResult result = _buildingController.CreateBuilding(BuildingDTO);
            int statusCode = (result as ObjectResult)?.StatusCode ?? 0;


            //assert
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

    }
}