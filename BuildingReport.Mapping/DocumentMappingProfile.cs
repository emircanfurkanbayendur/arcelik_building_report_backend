using AutoMapper;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;

public class DocumentMappingProfile : Profile
{
    public DocumentMappingProfile()
    {
        CreateMap<UpdateDocumentRequest, Document>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report))
            .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => src.UploadedAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.UploadedByUserId, opt => opt.MapFrom(src => src.UploadedByUserId))
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId));

        CreateMap<Document, UpdateDocumentRequest>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report))
            .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => src.UploadedAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.UploadedByUserId, opt => opt.MapFrom(src => src.UploadedByUserId))
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId));

        CreateMap<DocumentRequest, Document>()
            .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report))
            .ForMember(dest => dest.UploadedByUserId, opt => opt.MapFrom(src => src.UploadedByUserId))
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId));

        CreateMap<Document, DocumentRequest>()
            .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report))
            .ForMember(dest => dest.UploadedByUserId, opt => opt.MapFrom(src => src.UploadedByUserId))
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId));


        CreateMap<DocumentResponse, Document>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report))
            .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => src.UploadedAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.UploadedByUserId, opt => opt.MapFrom(src => src.UploadedByUserId))
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId));

        CreateMap<Document, DocumentResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report))
            .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => src.UploadedAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.UploadedByUserId, opt => opt.MapFrom(src => src.UploadedByUserId))
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId));

    }
}