using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
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
        public Building Post([FromBody] Building building) 
        {
            return _buildingService.CreateBuilding(building);
        }

        [HttpPut]
        public Building Put([FromBody] Building building)
        {
            return _buildingService.UpdateBuilding(building);
        }

        [HttpDelete]
        public void Delete(long id) 
        {
            _buildingService.DeleteBuilding(id);
        }

    }
}
