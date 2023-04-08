using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DataAccess.Abstract
{
    public interface IDocumentRepository
    {
        List<Document> GetAllDocuments();

        List<Document> GetDocumentsByBuildingId(long buildingId);
        List<Document> GetDocumentsByUserId(long userId);
        Document GetDocumentById(long id);

        Document CreateDocument(Document document);

        Document UpdateDocument(Document document);

        void DeleteDocument(long id);
        bool DocumentExistsById(long id);
    }
}
