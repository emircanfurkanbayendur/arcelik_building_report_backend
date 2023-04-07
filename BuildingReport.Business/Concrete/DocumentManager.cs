﻿using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
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
        public DocumentManager(IMapper mapper)
        {
            _documentRepository = new DocumentRepository();
            _mapper = mapper;
        }
        public Document CreateDocument(DocumentDTO documentDTO)
        {
            Document document = _mapper.Map<Document>(documentDTO);
            return _documentRepository.CreateDocument(document);
        }

        public void DeleteDocument(long id)
        {
            CheckIfDocumentExistsById(id);
            _documentRepository.DeleteDocument(id);
        }

        public List<Document> GetAllDocuments()
        {
            return _documentRepository.GetAllDocuments();
        }

        public Document GetDocumentById(long id)
        {
            CheckIfDocumentExistsById(id);
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

        public Document UpdateDocument(DocumentDTO documentDTO)
        {
            Document document = _mapper.Map<Document>(documentDTO);
            return _documentRepository.UpdateDocument(document);
        }

        public Document UpdateDocumentPatch(int id,JsonPatchDocument<DocumentDTO> patchdoc)
        {
            Document document = _documentRepository.GetDocumentById(id);
            if(document == null)
            {
                throw new Exception($"Document with ID {id} not found");
            }

            DocumentDTO documentDTO = _mapper.Map<DocumentDTO>(document);

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
