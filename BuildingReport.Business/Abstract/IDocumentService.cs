using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IDocumentService
    {
        List<Document> GetAllDocuments();

        Document GetDocumentById(long id);

        Document CreateDocument(Document document);

        Document UpdateDocument(Document document);

        void DeleteDocument(long id);
    }
}
