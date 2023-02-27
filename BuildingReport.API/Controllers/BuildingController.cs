using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private IBuildingService _buildingService;

        public BuildingController()
        {
            _buildingService = new BuildingManager();
        }

        [HttpGet]
        public List<Building> GetBuildings()
        {
            return _buildingService.GetAllBuildings();
        }

        [HttpGet("{id}")]
        public Building GetBuildings(long id)
        {
            return _buildingService.GetBuildingById(id);
        }

        [HttpPost]
        public Building Post([FromBody] BuildingDTO buildingdto) 
        {
            var building = new Building()
            {
                Id = buildingdto.Id,
                Name = buildingdto.Name,
                Adress = buildingdto.Adress,
                Code = buildingdto.Code,
                Latitude = buildingdto.Latitude,
                Longitude = buildingdto.Longitude,
                RegisteredAt = buildingdto.RegisteredAt,
                IsActive = buildingdto.IsActive,
                CreatedByUserId = buildingdto.CreatedByUserId
            };

            return _buildingService.CreateBuilding(building);
        }

        [HttpPut]
        public Building Put([FromBody] BuildingDTO buildingdto)
        {
            var building = new Building()
            {
                Id = buildingdto.Id,
                Name = buildingdto.Name,
                Adress = buildingdto.Adress,
                Code = buildingdto.Code,
                Latitude = buildingdto.Latitude,
                Longitude = buildingdto.Longitude,
                RegisteredAt = buildingdto.RegisteredAt,
                IsActive = buildingdto.IsActive,
                CreatedByUserId = buildingdto.CreatedByUserId
            };

            return _buildingService.UpdateBuilding(building);
        }

        [HttpDelete]
        public void Delete(long id) 
        {
            _buildingService.DeleteBuilding(id);
        }

    }
}
