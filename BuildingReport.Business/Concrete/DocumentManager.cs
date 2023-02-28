using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{
    public class DocumentManager : IDocumentService
    {
        private IDocumentRepository _documentRepository;

        public DocumentManager()
        {
            _documentRepository = new DocumentRepository();
        }
        public Document CreateDocument(Document document)
        {
            return _documentRepository.CreateDocument(document);
        }

        public void DeleteDocument(long id)
        {
            _documentRepository.DeleteDocument(id);
        }

        public List<Document> GetAllDocuments()
        {
            return _documentRepository.GetAllDocuments();
        }

        public Document GetDocumentById(long id)
        {
            return _documentRepository.GetDocumentById(id);
        }

        public List<Document> GetDocumentsByBuildingId(long buildingId)
        {
            return _documentRepository.GetDocumentsByBuildingId(buildingId);
        }

        public List<Document> GetDocumentsByUserId(long userId)
        {
            return _documentRepository.GetDocumentsByUserId(userId);
        }

        public Document UpdateDocument(Document document)
        {
            return _documentRepository.UpdateDocument(document);
        }
    }
}
