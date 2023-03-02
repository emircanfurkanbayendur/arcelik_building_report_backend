﻿using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
    [Authorize]
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

        [HttpGet("user/{userId}")]
        public List<Building> GetBuildingsByUserId(long userId)
        {
            return _buildingService.GetBuildingsByUserId(userId);
        }

        [HttpGet("adress/{adress}")]
        public Building GetBuildingsByAdress(string adress)
        {
            return _buildingService.GetBuildingByAdress(adress);
        }

        [HttpGet("code/{code}")]
        public Building GetBuildingsByCode(string code)
        {
            return _buildingService.GetBuildingByCode(code);
        }

        [HttpGet("city/{city}")]
        public List<Building> GetBuildingsByCity(string city)
        {
            return _buildingService.GetBuildingByCity(city);
        }

        [HttpGet("district/{district}")]
        public List<Building> GetBuildingsByDistrict(string district)
        {
            return _buildingService.GetBuildingByDistrict(district);
        }

        [HttpGet("neighbourhood/{neighbourhood}")]
        public List<Building> GetBuildingsByNeighbourhood(string neighbourhood)
        {
            return _buildingService.GetBuildingByNeighbourhood(neighbourhood);
        }

        [HttpPost]
        public IActionResult CreateBuilding([FromBody] BuildingDTO buildingdto) 
        {
            if (buildingdto == null)
            {
                return BadRequest(ModelState);
            }

            var building = new Building()
            {
                Id = buildingdto.Id,
                Name = buildingdto.Name,
                Adress = buildingdto.Adress,
                City = buildingdto.City,
                District = buildingdto.District,
                Neighbourhood = buildingdto.Neighbourhood,
                Code = buildingdto.Code,
                Latitude = buildingdto.Latitude,
                Longitude = buildingdto.Longitude,
                RegisteredAt = buildingdto.RegisteredAt,
                IsActive = buildingdto.IsActive,
                CreatedByUserId = buildingdto.CreatedByUserId
            };

            if(_buildingService.BuildingExists(building.Code, building.Adress))
            {
                ModelState.AddModelError("", "Building already exists");
                return StatusCode(422, ModelState);
            }

            _buildingService.CreateBuilding(building);

            return Ok("Successfuly created");
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
