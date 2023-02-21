using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace arcelik_building_report_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly ArcelikBuildingReportDbContext _context;

        public BuildingController(ArcelikBuildingReportDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBuildings()
        {
            return Ok(await _context.Buildings.ToListAsync());
        }
    }
}
