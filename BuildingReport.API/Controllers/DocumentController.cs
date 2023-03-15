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

        public DocumentController()
        {
            _documentService = new DocumentManager();
        }

        [AllowAnonymous]
        [HttpGet]
        public List<Document> GetDocuments()
        {
            return _documentService.GetAllDocuments();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public Document GetDocuments(long id)
        {
            return _documentService.GetDocumentById(id);
        }

        [AllowAnonymous]
        [HttpGet("building/{buildingId}")]
        public List<Document> GetDocumentsByBuildingID(long buildingId)
        {
            return _documentService.GetDocumentsByBuildingId(buildingId);
        }

        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        public List<Document> GetDocumentsByUserID(long userId)
        {
            return _documentService.GetDocumentsByUserId(userId);
        }

        [HttpPost]
        public Document Post([FromBody] DocumentDTO documentdto)
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

            return _documentService.CreateDocument(document);
        }

        [HttpPut]
        public Document Put([FromBody] DocumentDTO documentdto)
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

            return _documentService.UpdateDocument(document);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _documentService.DeleteDocument(id);
        }
    }
}
