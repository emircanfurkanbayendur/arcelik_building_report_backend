using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
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
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetDocuments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_documentService.GetAllDocuments());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetDocumentById(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_documentService.GetDocumentById(id));
        }

        [AllowAnonymous]
        [HttpGet("building/{buildingId}")]
        public IActionResult GetDocumentsByBuildingID(long buildingId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_documentService.GetDocumentsByBuildingId(buildingId));
        }

        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        public IActionResult GetDocumentsByUserID(long userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_documentService.GetDocumentsByUserId(userId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] DocumentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _documentService.CreateDocument(request);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateDocumentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _documentService.UpdateDocument(request);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, [FromBody] JsonPatchDocument<PatchDocumentRequest> pathdoc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_documentService.UpdateDocumentPatch(id, pathdoc));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _documentService.DeleteDocument(id);

            if (!response)
                return Unauthorized();

            return NoContent();
        }
    }
}
