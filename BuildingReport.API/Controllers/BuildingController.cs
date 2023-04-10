using AutoMapper;
using Azure;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [AllowAnonymous]
        [HttpGet("streetByAdress")]
        public IActionResult GetStreetsByCityDistrictNeighbourhood(string city, string district, string neighbourhood)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetStreetsByCityDistrictNeighbourhood(city, district, neighbourhood));
        }

        [AllowAnonymous]
        [HttpGet("buildingsByAdress")]
        public IActionResult GetBuildingsByCityDistrictNeighbourhoodStreet(string city, string district, string neighbourhood, string street)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingsByCityDistrictNeighbourhoodStreet(city, district, neighbourhood, street));
        }

        [AllowAnonymous]
        [HttpGet("nameBuildingNumberByAdress")]
        public IActionResult GetBuildingNameBuildingNumber(string city, string district, string neighbourhood, string street)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.GetBuildingNameBuildingNumbers(city, district, neighbourhood, street));
        }


        [HttpPost]
        public IActionResult Create([FromBody] BuildingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _buildingService.CreateBuilding(request);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateBuildingRequest buildingdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _buildingService.UpdateBuilding(buildingdto);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id,[FromBody] JsonPatchDocument<UpdateBuildingRequest> patchdoc)
        {
            if (patchdoc == null)
                return BadRequest(ModelState);

            return Ok(_buildingService.UpdateBuildingPatch(id,patchdoc));
        }

        [HttpDelete]
        public IActionResult Delete(long id) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _buildingService.DeleteBuilding(id);

            if (!response)
                return Unauthorized();

            return NoContent();
        }

    }
}
