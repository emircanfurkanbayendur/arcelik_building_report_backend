using BuildingReport.Business.Abstract;
using BuildingReport.Business.Concrete;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingReport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentController()
        {
            _documentService = new DocumentManager();
        }

        [HttpGet]
        public List<Document> GetDocuments()
        {
            return _documentService.GetAllDocuments();
        }

        [HttpGet("{id}")]
        public Document GetDocuments(long id)
        {
            return _documentService.GetDocumentById(id);
        }

        [HttpPost]
        public Document Post([FromBody] Document document)
        {
            return _documentService.CreateDocument(document);
        }

        [HttpPut]
        public Document Put([FromBody] Document document)
        {
            return _documentService.UpdateDocument(document);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _documentService.DeleteDocument(id);
        }
    }
}
