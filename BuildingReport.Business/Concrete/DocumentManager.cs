using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.CustomExceptionMiddleware.DocumentExceptions;
using BuildingReport.Business.CustomExceptionMiddleware.IdExceptions;
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
        private readonly IBuildingService _buildingService;
        private readonly IUserService _userService;
        public DocumentManager(IMapper mapper, IRoleAuthorityService roleAuthorityService, IBuildingService buildingService, IUserService userService)
        {
            _documentRepository = new DocumentRepository();
            _mapper = mapper;
            _roleAuthorityService = roleAuthorityService;
            _buildingService = buildingService;
            _userService = userService;
        }
        public DocumentResponse CreateDocument(DocumentRequest request)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 2))
            {
                return null;
            }

            _ = request ?? throw new ArgumentNullException(nameof(request)," cannot be null");
            //Document çok büyük olabildiği için o document veritabanında var mı kontrolü yapılmıyor.

            _userService.CheckIfUserExistsById(request.UploadedByUserId);
            _buildingService.CheckIfBuildingExistsById(request.BuildingId);

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

            ValidateId(id);

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
            ValidateId(id);
            CheckIfDocumentExistsById(id);
            DocumentResponse response = _mapper.Map<DocumentResponse>(_documentRepository.GetDocumentById(id));
            return response;
        }

        public List<DocumentResponse> GetDocumentsByBuildingId(long buildingId)
        {
            ValidateId(buildingId);
            _buildingService.CheckIfBuildingExistsById(buildingId);
            List<DocumentResponse> response = _mapper.Map<List<DocumentResponse>>(_documentRepository.GetDocumentsByBuildingId(buildingId));
            return response;
        }

        public List<DocumentResponse> GetDocumentsByUserId(long userId)
        {
            ValidateId(userId);
            _userService.CheckIfUserExistsById(userId);
            List<DocumentResponse> response = _mapper.Map<List<DocumentResponse>>(_documentRepository.GetDocumentsByUserId(userId));
            return response;
        }

        public DocumentResponse UpdateDocument(UpdateDocumentRequest documentDTO)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 4))
            {
                return null;
            }


            _ = documentDTO ?? throw new ArgumentNullException(nameof(documentDTO), " cannot be null");

            ValidateId(documentDTO.Id);
            ValidateId(documentDTO.BuildingId);
            ValidateId(documentDTO.UploadedByUserId);


            CheckIfDocumentExistsById(documentDTO.Id);
            _userService.CheckIfUserExistsById(documentDTO.UploadedByUserId);
            _buildingService.CheckIfBuildingExistsById(documentDTO.BuildingId);
            Document document = _mapper.Map<Document>(documentDTO);
            DocumentResponse response = _mapper.Map<DocumentResponse>(_documentRepository.UpdateDocument(document));
            return response;
        }

        public Document UpdateDocumentPatch(int id, JsonPatchDocument<PatchDocumentRequest> patchdoc)
        {
            ValidateId(id);
                

            CheckIfDocumentExistsById(id);
          
            Document document = _documentRepository.GetDocumentById(id);
            PatchDocumentRequest documentDTO = _mapper.Map<PatchDocumentRequest>(document);


            patchdoc.ApplyTo(documentDTO);

            if (documentDTO.BuildingId <= 0 || documentDTO.UploadedByUserId <= 0)
            {
                throw new ArgumentOutOfRangeException(documentDTO.BuildingId <= 0 ? nameof(documentDTO.BuildingId) : nameof(documentDTO.UploadedByUserId), " cannot be less than or equal to 0.");
            }

            _buildingService.CheckIfBuildingExistsById(documentDTO.BuildingId);
            _userService.CheckIfUserExistsById(documentDTO.UploadedByUserId);

            document = _mapper.Map<Document>(documentDTO);

            return _documentRepository.UpdateDocument(document);

        }





        //BusinessRules
        public void CheckIfDocumentExistsById(long id)
        {
            if (!_documentRepository.DocumentExistsById(id))
            {
                throw new DocumentNotFoundException("document cannot be found.");
            }
        }

        private void ValidateId(long id)
        {
            if (id <= 0 || id > long.MaxValue)
            {
                throw new IdOutOfRangeException(nameof(id), id);
            }
        }
    }
}
