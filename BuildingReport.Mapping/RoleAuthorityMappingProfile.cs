using AutoMapper;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;

public class RoleAuthorityMappingProfile : Profile
{
    public RoleAuthorityMappingProfile()
    {
        CreateMap<UpdateRoleAuthorityRequest, RoleAuthority>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.AuthorityId, opt => opt.MapFrom(src => src.AuthorityId));

        CreateMap<RoleAuthority, UpdateRoleAuthorityRequest>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.AuthorityId, opt => opt.MapFrom(src => src.AuthorityId));

        CreateMap<RoleAuthorityRequest, RoleAuthority>()
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.AuthorityId, opt => opt.MapFrom(src => src.AuthorityId));

        CreateMap<RoleAuthority, RoleAuthorityRequest>()
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.AuthorityId, opt => opt.MapFrom(src => src.AuthorityId));

        CreateMap<RoleAuthorityResponse, RoleAuthority>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
           .ForMember(dest => dest.AuthorityId, opt => opt.MapFrom(src => src.AuthorityId));

        CreateMap<RoleAuthority, RoleAuthorityResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.AuthorityId, opt => opt.MapFrom(src => src.AuthorityId));
    }
}
