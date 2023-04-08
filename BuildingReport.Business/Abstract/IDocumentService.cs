using BuildingReport.DTO;
using BuildingReport.Entities;
using Microsoft.AspNetCore.JsonPatch;
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
        List<Document> GetDocumentsByBuildingId(long buildingId);
        List<Document> GetDocumentsByUserId(long userId);
        Document GetDocumentById(long id);
        Document CreateDocument(DocumentDTO documentDTO);
        Document UpdateDocument(DocumentDTO documentDTO);
        Document UpdateDocumentPatch(int id, JsonPatchDocument<DocumentDTO> pathdoc);
        void DeleteDocument(long id);
        void CheckIfDocumentExistsById(long id);
    }
}
