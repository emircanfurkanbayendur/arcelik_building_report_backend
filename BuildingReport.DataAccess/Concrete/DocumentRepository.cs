using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAcess;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DataAccess.Concrete
{
    public class DocumentRepository : IDocumentRepository
    {
        public Document CreateDocument(Document document)
        {
            using (var documentDbContext = new ArcelikBuildingReportDbContext())
            {
                documentDbContext.Documents.Add(document);
                documentDbContext.SaveChanges();
                return document;
            }
        }

        public void DeleteDocument(long id)
        {
            var document = new Document() { Id = id, IsActive = false };
            using (var documentDbContext = new ArcelikBuildingReportDbContext())
            {

                documentDbContext.Attach(document);
                documentDbContext.Entry(document).Property(x => x.IsActive).IsModified = true;
                documentDbContext.SaveChanges();



            }
        }

        public List<Document> GetAllDocuments()
        {
            using(var documentDbContext = new ArcelikBuildingReportDbContext())
            {
                return documentDbContext.Documents.ToList();
            }
        }

        public Document GetDocumentById(long id)
        {
            using (var documentDbContext = new ArcelikBuildingReportDbContext())
            {
                return documentDbContext.Documents.Find(id);
            }
        }

        public List<Document> GetDocumentsByBuildingId(long buildingId)
        {
            using (var documentDbContext = new ArcelikBuildingReportDbContext())
            {
                return documentDbContext.Documents.Where(d => d.BuildingId == buildingId).ToList();
            }
        }

        public List<Document> GetDocumentsByUserId(long userId)
        {
            using (var documentDbContext = new ArcelikBuildingReportDbContext())
            {
                return documentDbContext.Documents.Where(d => d.UploadedByUserId == userId).ToList();
            }
        }

        public Document UpdateDocument(Document document)
        {
            using (var documentDbContext = new ArcelikBuildingReportDbContext())
            {
                documentDbContext.Documents.Update(document);
                documentDbContext.SaveChanges();
                return document;
            }
        }
    }
}
