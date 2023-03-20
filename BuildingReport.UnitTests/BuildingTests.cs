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


        [Test]
        public void GetBuildings_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _buildingController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _buildingController.GetBuildings();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetBuildings_WithValidModelState_ReturnsOK()
        {
            //arrange


            //action
            IActionResult result = _buildingController.GetBuildings();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetBuildingsWithID_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _buildingController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _buildingController.GetBuildings();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetBuildingsWithID_WithValidModelState_ReturnsOK()
        {
            //arrange
            Building building = new Building()
            {
                Id = 1,
                Name = "Test",

            };

            _buildingServiceMock.Setup(i => i.GetBuildingById(1)).Returns(building);

            //action
            IActionResult result = _buildingController.GetBuildings();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UpdateBuilding_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            BuildingDTO buildingDTO = new BuildingDTO()
            {
                Id = 1,
                Name = "Test",

            };
            _buildingController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _buildingController.Put(buildingDTO);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void UpdateBuilding_WithValidModelState_ReturnsOK()
        {
            //arrange
            BuildingDTO buildingDTO = new BuildingDTO()
            {
                Id = 1,
                Name = "Test",

            };

            Building building = new Building()
            {
                Id = 1,
                Name = "Test",

            };


            _buildingServiceMock.Setup(i => i.UpdateBuilding(building)).Returns(building);

            //action
            IActionResult result = _buildingController.Put(buildingDTO);



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void DeleteBuilding_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            Building building = new Building()
            {
                Id = 1,
                Name = "Test",

            };
            _buildingController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _buildingController.Delete(1);



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void DeleteBuilding_WithValidModelState_ReturnsOK()
        {
            //arrange
            Building building = new Building()
            {
                Id = 1,
                Name = "Test",

            };



            //action
            IActionResult result = _buildingController.Delete(building.Id);



            //assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }


        [Test]
        public void GetBuildingCounts_WithInvalidModelState_ReturnsBadRequest()
        {
            //arrange
            _buildingController.ModelState.AddModelError("Test", "InvalidModelError");


            //action
            IActionResult result = _buildingController.GetBuildingCounts();



            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GetBuildingCounts_WithValidModelState_ReturnsOK()
        {
            //arrange
            Building building = new Building()
            {
                Id = 1,
                Name = "Test",

            };


            List<int> counts = new List<int> { 1, 2, 3, 4 };

            _buildingServiceMock.Setup(i => i.GetBuildingCounts()).Returns(counts);

            //action
            
            IActionResult result = _buildingController.GetBuildingCounts();



            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}