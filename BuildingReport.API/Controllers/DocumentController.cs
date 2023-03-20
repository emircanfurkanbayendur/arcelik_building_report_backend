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
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
            //_documentService = new DocumentManager();
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
        public IActionResult GetDocuments(long id)
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
        public IActionResult Post([FromBody] DocumentDTO documentdto)
        {
            if (documentdto == null)
            {
                return BadRequest(ModelState);
            }

            if (_documentService.DocumentExists(documentdto.Report))
            {
                ModelState.AddModelError("", "Document already exists");
                return StatusCode(422, ModelState);
            }

            var document = new Document()
            {
                Id = documentdto.Id,
                Report = documentdto.Report,
                UploadedAt = documentdto.UploadedAt,
                IsActive = documentdto.IsActive,
                UploadedByUserId = documentdto.UploadedByUserId,
                BuildingId = documentdto.BuildingId,

            };

            return Ok(_documentService.CreateDocument(document));
        }

        [HttpPut]
        public IActionResult Put([FromBody] DocumentDTO documentdto)
        {
            var document = new Document()
            {
                Id = documentdto.Id,
                Report = documentdto.Report,
                UploadedAt = documentdto.UploadedAt,
                IsActive = documentdto.IsActive,
                UploadedByUserId = documentdto.UploadedByUserId,
                BuildingId = documentdto.BuildingId,

            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_documentService.UpdateDocument(document));
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
