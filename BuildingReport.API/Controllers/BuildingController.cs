using AutoMapper;
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
        private readonly IMapper _mapper;
        

        public BuildingController(IBuildingService buildingService, IMapper mapper)
        {
            _buildingService = buildingService;
            _mapper = mapper;
            //_buildingService = new BuildingManager();
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
        public IActionResult GetBuildings(long id)
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

            List<int> counts = _buildingService.GetBuildingCounts();
            BuildingCountDTO buildingCountDto = _mapper.Map<BuildingCountDTO>(counts);
            //BuildingCountDto buildingCountDto = new BuildingCountDto()
            //{
            //    CityCount = counts[0],
            //    DistrictCount = counts[1],
            //    NeighbourhoodCount = counts[2],
            //    BuildingCount = counts[3],

            //};
            return Ok(buildingCountDto);
        }

        [HttpPost]
        public IActionResult CreateBuilding([FromBody] BuildingDTO buildingdto) 
        {
            if (buildingdto == null)
            {
                return BadRequest(ModelState);
            }

            Building building = _mapper.Map<Building>(buildingdto);



            if(_buildingService.BuildingExists(building.Code))
            {
                ModelState.AddModelError("", "Building already exists");
                return StatusCode(422, ModelState);
            }

            _buildingService.CreateBuilding(building);

            return Ok(building);
        }

        [HttpPut]
        public IActionResult Put([FromBody] BuildingDTO buildingdto)
        {
            Building building = _mapper.Map<Building>(buildingdto);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_buildingService.UpdateBuilding(building));
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
