using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.DTO;
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
        public IActionResult Create([FromBody] DocumentDTO documentdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_documentService.CreateDocument(documentdto));
        }

        [HttpPut]
        public IActionResult Update([FromBody] DocumentDTO documentdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_documentService.UpdateDocument(documentdto));
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, [FromBody] JsonPatchDocument<DocumentDTO> pathdoc)
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

            _documentService.DeleteDocument(id);

            return NoContent();
        }
    }
}
