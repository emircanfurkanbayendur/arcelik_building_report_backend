using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper _mapper;
        private readonly IRoleAuthorityService _roleAuthorityService;
        public DocumentManager(IMapper mapper, IRoleAuthorityService roleAuthorityService)
        {
            _documentRepository = new DocumentRepository();
            _mapper = mapper;
            _roleAuthorityService = roleAuthorityService;
        }
        public DocumentResponse CreateDocument(DocumentRequest request)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 2))
            {
                return null;
            }
            Document document = _mapper.Map<Document>(request);
            document.UploadedAt = DateTime.Now;
            document.IsActive = true;
            DocumentResponse response = _mapper.Map<DocumentResponse>(_documentRepository.CreateDocument(document));
            return response;
        }

        public bool DeleteDocument(long id)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 3))
            {
                return false;
            }
            CheckIfDocumentExistsById(id);
            _documentRepository.DeleteDocument(id);
            return true;
        }

        public List<DocumentResponse> GetAllDocuments()
        {
            List<DocumentResponse> response = _mapper.Map<List<DocumentResponse>>(_documentRepository.GetAllDocuments());
            return response;
        }

        public DocumentResponse GetDocumentById(long id)
        {
            CheckIfDocumentExistsById(id);
            DocumentResponse response = _mapper.Map<DocumentResponse>(_documentRepository.GetDocumentById(id));
            return response;
        }

        public List<DocumentResponse> GetDocumentsByBuildingId(long buildingId)
        {
            List<DocumentResponse> response = _mapper.Map<List<DocumentResponse>>(_documentRepository.GetDocumentsByBuildingId(buildingId));
            return response;
        }

        public List<DocumentResponse> GetDocumentsByUserId(long userId)
        {
            List<DocumentResponse> response = _mapper.Map<List<DocumentResponse>>(_documentRepository.GetDocumentsByUserId(userId));
            return response;
        }

        public DocumentResponse UpdateDocument(UpdateDocumentRequest documentDTO)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 4))
            {
                return null;
            }
            Document document = _mapper.Map<Document>(documentDTO);
            DocumentResponse response = _mapper.Map<DocumentResponse>(_documentRepository.UpdateDocument(document));
            return response;
        }

        public Document UpdateDocumentPatch(int id,JsonPatchDocument<UpdateDocumentRequest> patchdoc)
        {
            Document document = _documentRepository.GetDocumentById(id);
            if(document == null)
            {
                throw new Exception($"Document with ID {id} not found");
            }

            UpdateDocumentRequest documentDTO = _mapper.Map<UpdateDocumentRequest>(document);

            patchdoc.ApplyTo(documentDTO);

            document = _mapper.Map(documentDTO, document);

            Document document2 = _documentRepository.UpdateDocument(document);

            return document2;

        }

        //BusinessRules
        public void CheckIfDocumentExistsById(long id)
        {
            if (!_documentRepository.DocumentExistsById(id))
            {
                throw new NotImplementedException("document cannot find.");
            }
        }
    }
}
