using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
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
        List<DocumentResponse> GetAllDocuments();
        List<DocumentResponse> GetDocumentsByBuildingId(long buildingId);
        List<DocumentResponse> GetDocumentsByUserId(long userId);
        DocumentResponse GetDocumentById(long id);
        DocumentResponse CreateDocument(DocumentRequest request);
        DocumentResponse UpdateDocument(UpdateDocumentRequest request);
        Document UpdateDocumentPatch(int id, JsonPatchDocument<UpdateDocumentRequest> pathdoc);
        bool DeleteDocument(long id);
        void CheckIfDocumentExistsById(long id);
    }
}
