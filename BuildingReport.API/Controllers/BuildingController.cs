﻿using AutoMapper;
using BuildingReport.Business.Abstract;
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

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }


        [HttpGet]
        public IActionResult GetBuildings()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetAllBuildings());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetBuildingById(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingById(id));
        }

        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        public IActionResult GetBuildingsByUserId(long userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingsByUserId(userId));
        }


        [AllowAnonymous]
        [HttpGet("code/{code}")]
        public IActionResult GetBuildingsByCode(string code)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingByCode(code));
        }

        [AllowAnonymous]
        [HttpGet("city/{city}")]
        public IActionResult GetBuildingsByCity(string city)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingByCity(city));
        }

        [AllowAnonymous]
        [HttpGet("district/{district}")]
        public IActionResult GetBuildingsByDistrict(string district)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingByDistrict(district));
        }
        [AllowAnonymous]
        [HttpGet("neighbourhood/{neighbourhood}")]
        public IActionResult GetBuildingsByNeighbourhood(string neighbourhood)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingByNeighbourhood(neighbourhood));
        }

        [AllowAnonymous]
        [HttpGet("street/{street}")]
        public IActionResult GetBuildingsByStreet(string street)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingByStreet(street));
        }

        [AllowAnonymous]
        [HttpGet("count")]
        public IActionResult GetBuildingCounts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingCounts());
        }

        [HttpPost]
        public IActionResult Create([FromBody] BuildingDTO buildingdto) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.CreateBuilding(buildingdto));
        }

        [HttpPut]
        public IActionResult Update([FromBody] BuildingDTO buildingdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.UpdateBuilding(buildingdto));
        }

        [HttpDelete]
        public IActionResult Delete(long id) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _buildingService.DeleteBuilding(id);

            return NoContent();
        }

    }
}
